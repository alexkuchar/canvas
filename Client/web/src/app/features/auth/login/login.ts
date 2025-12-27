import { Component, inject, signal } from '@angular/core';
import {
  MatCard,
  MatCardContent,
  MatCardFooter,
  MatCardHeader,
  MatCardSubtitle,
  MatCardTitle,
} from '@angular/material/card';
import { MatSnackBar, MatSnackBarActions } from '@angular/material/snack-bar';
import { Router, RouterLink } from '@angular/router';
import { MatFormField, MatLabel, MatError } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { AuthService } from '../../../core/auth/auth.service';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { LoginRequest } from '../../../core/auth/auth.types';
import { HttpErrorResponse } from '@angular/common/http';
import { getErrorMessage } from '../../../core/http-error.util';

@Component({
  selector: 'app-login',
  imports: [
    MatCard,
    MatCardHeader,
    MatCardTitle,
    MatCardSubtitle,
    MatCardContent,
    MatCardFooter,
    RouterLink,
    MatFormField,
    MatInput,
    MatButton,
    MatIcon,
    MatIconButton,
    ReactiveFormsModule,
    MatLabel,
    MatError,
  ],
  templateUrl: './login.html',
})
export class Login {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly snackBar = inject(MatSnackBar);

  hide = signal(true);

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }

    this.authService.login(this.loginForm.value as unknown as LoginRequest).subscribe({
      next: () => {
        this.router.navigate(['/dashboard']);
        this.loginForm.reset();
      },
      error: (error: HttpErrorResponse) => {
        this.snackBar.open(getErrorMessage(error), 'Close', {
          duration: 5000,
        });
        console.error(error);
      },
    });
  }
}
