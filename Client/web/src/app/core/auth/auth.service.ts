import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
  LoginRequest,
  LoginResponse,
  RegisterRequest,
  RefreshRequest,
  RefreshResponse,
  User,
  ForgotPasswordRequest,
  ResetPasswordRequest,
  VerifyUserRequest,
  Profile,
} from './auth.types';
import { environment } from '../../../environments/environment';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { UpdateUserResponse } from '../user/user.types';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;

  private readonly userSubject = new BehaviorSubject<User | null>(null);
  public readonly user$ = this.userSubject.asObservable();

  get user() {
    return this.userSubject.value;
  }

  constructor() {
    const storedUser = localStorage.getItem('user');
    const storedTokens = localStorage.getItem('tokens');

    if (storedUser && storedTokens) {
      try {
        const user: User = {
          user: JSON.parse(storedUser),
          tokens: JSON.parse(storedTokens),
        };
        this.userSubject.next(user);
      } catch (error) {
        localStorage.removeItem('user');
        localStorage.removeItem('tokens');
        this.userSubject.next(null);
      }
    }
  }

  register(request: RegisterRequest) {
    return this.http.post<void>(`${this.baseUrl}/api/Auth/register`, request);
  }

  verifyUser(request: VerifyUserRequest) {
    return this.http.post<void>(`${this.baseUrl}/api/Auth/verify`, request);
  }

  login(request: LoginRequest) {
    return this.http.post<LoginResponse>(`${this.baseUrl}/api/Auth/login`, request).pipe(
      tap((response) => {
        const { user, tokens } = response;

        localStorage.setItem('user', JSON.stringify(user));
        localStorage.setItem('tokens', JSON.stringify(tokens));

        this.userSubject.next({
          user,
          tokens,
        });
      })
    );
  }

  refresh(): Observable<RefreshResponse> {
    const refreshToken = this.user?.tokens.refreshToken;
    if (!refreshToken) {
      throw new Error('No refresh token available');
    }

    return this.http
      .post<RefreshResponse>(`${this.baseUrl}/api/Auth/refresh`, {
        refreshToken,
      } as RefreshRequest)
      .pipe(
        tap((response) => {
          const currentUser = this.user;
          if (currentUser) {
            const updatedUser: User = {
              user: currentUser.user,
              tokens: {
                accessToken: response.accessToken,
                refreshToken: response.refreshToken,
                refreshTokenExpiresAt: response.refreshTokenExpiresAt,
              },
            };

            localStorage.setItem('tokens', JSON.stringify(updatedUser.tokens));
            this.userSubject.next(updatedUser);
          }
        })
      );
  }

  forgotPassword(request: ForgotPasswordRequest) {
    return this.http.post<void>(`${this.baseUrl}/api/Auth/forgot-password`, request);
  }

  resetPassword(request: ResetPasswordRequest) {
    return this.http.post<void>(`${this.baseUrl}/api/Auth/reset-password`, request);
  }

  updateUser(user: UpdateUserResponse) {
    this.userSubject.next({
      user: {
        ...this.user?.user,
        ...user,
      },
      tokens: this.user?.tokens ?? {
        accessToken: '',
        refreshToken: '',
        refreshTokenExpiresAt: new Date(),
      },
    });

    localStorage.setItem(
      'user',
      JSON.stringify({
        ...this.user?.user,
        ...user,
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('tokens');
    this.userSubject.next(null);
  }
}
