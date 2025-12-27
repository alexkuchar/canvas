import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import {
  MatCard,
  MatCardTitle,
  MatCardHeader,
  MatCardSubtitle,
  MatCardContent,
  MatCardFooter,
} from '@angular/material/card';
import { MatFormField, MatLabel, MatError } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AuthService } from '../../../core/auth/auth.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ForgotPasswordRequest } from '../../../core/auth/auth.types';
import { HttpErrorResponse } from '@angular/common/http';
import { getErrorMessage } from '../../../core/http-error.util';

@Component({
  selector: 'app-forgot-password',
  imports: [
    MatCard,
    MatCardTitle,
    MatCardHeader,
    MatCardSubtitle,
    MatCardContent,
    MatFormField,
    MatLabel,
    MatInput,
    MatCardFooter,
    MatButton,
    ReactiveFormsModule,
    MatError,
  ],
  templateUrl: './forgot-password.html',
  styleUrls: ['./forgot-password.scss'],
})
export class ForgotPassword {
  private readonly authService = inject(AuthService);
  private readonly snackBar = inject(MatSnackBar);

  forgotPasswordForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  onSubmit() {
    if (this.forgotPasswordForm.invalid) {
      return;
    }

    this.authService
      .forgotPassword(this.forgotPasswordForm.value as unknown as ForgotPasswordRequest)
      .subscribe({
        next: () => {
          this.snackBar.open('Password reset email sent', 'Close', { duration: 5000 });
        },
        error: (error: HttpErrorResponse) => {
          this.snackBar.open(getErrorMessage(error), 'Close', { duration: 5000 });
          console.error(error);
        },
      });
  }
}
