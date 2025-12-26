import { HttpInterceptorFn, HttpErrorResponse, HttpEvent } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { catchError, switchMap, throwError, Observable } from 'rxjs';

let isRefreshing = false;
let refreshTokenSubject: Observable<HttpEvent<unknown>> | null = null;

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (
    req.url.includes('/api/Auth/login') ||
    req.url.includes('/api/Auth/register') ||
    req.url.includes('/api/Auth/refresh')
  ) {
    return next(req);
  }

  const user = authService.user;
  if (user?.tokens?.accessToken) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${user.tokens.accessToken}`,
      },
    });
  }

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && user?.tokens?.refreshToken) {
        if (isRefreshing && refreshTokenSubject) {
          return refreshTokenSubject.pipe(
            switchMap(() => {
              const updatedUser = authService.user;
              if (updatedUser?.tokens?.accessToken) {
                req = req.clone({
                  setHeaders: {
                    Authorization: `Bearer ${updatedUser.tokens.accessToken}`,
                  },
                });
              }
              return next(req);
            })
          );
        }

        isRefreshing = true;
        refreshTokenSubject = authService.refresh().pipe(
          switchMap(() => {
            isRefreshing = false;
            refreshTokenSubject = null;

            const updatedUser = authService.user;
            if (updatedUser?.tokens?.accessToken) {
              req = req.clone({
                setHeaders: {
                  Authorization: `Bearer ${updatedUser.tokens.accessToken}`,
                },
              });
            }
            return next(req);
          }),
          catchError((refreshError) => {
            isRefreshing = false;
            refreshTokenSubject = null;
            authService.logout();
            router.navigate(['/login']);
            return throwError(() => refreshError);
          })
        );

        return refreshTokenSubject;
      }

      return throwError(() => error);
    })
  );
};
