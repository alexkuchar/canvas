import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { UpdateUserRequest, UpdateUserResponse } from './user.types';
import { AuthService } from '../auth/auth.service';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly authService = inject(AuthService);
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;

  updateUser(request: UpdateUserRequest) {
    return this.http
      .put<UpdateUserResponse>(`${this.baseUrl}/api/Users/update`, {
        id: request.id,
        firstName: request.firstName,
        lastName: request.lastName,
        email: request.email,
        password: request.password,
      })
      .pipe(tap((response) => this.authService.updateUser(response as UpdateUserResponse)));
  }

  deleteUser() {}
}
