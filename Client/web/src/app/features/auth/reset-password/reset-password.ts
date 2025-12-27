import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import {
  MatCard,
  MatCardContent,
  MatCardFooter,
  MatCardHeader,
  MatCardSubtitle,
  MatCardTitle,
} from '@angular/material/card';
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AuthService } from '../../../core/auth/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { getErrorMessage } from '../../../core/http-error.util';

@Component({
  selector: 'app-reset-password',
  imports: [
    MatCard,
    MatCardHeader,
    MatCardTitle,
    MatCardSubtitle,
    MatCardContent,
    MatFormField,
    MatInput,
    MatButton,
    ReactiveFormsModule,
    MatCardFooter,
    MatError,
    MatLabel,
  ],
  templateUrl: './reset-password.html',
})
export class ResetPassword {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly snackBar = inject(MatSnackBar);

  private readonly route = inject(ActivatedRoute);

  private readonly token = (() => {
    const token = this.route.snapshot.queryParamMap.get('token');
    if (!token) {
      this.snackBar.open('Invalid token', 'Close', { duration: 5000 });
      return null;
    }
    return token;
  })();

  resetPasswordForm = new FormGroup({
    newPassword: new FormControl('', [Validators.required, Validators.minLength(8)]),
  });

  onCancel() {
    this.router.navigate(['/login']);
  }

  onSubmit() {
    if (this.resetPasswordForm.invalid) {
      return;
    }

    this.authService
      .resetPassword({
        token: this.token as string,
        newPassword: this.resetPasswordForm.value.newPassword ?? '',
      })
      .subscribe({
        next: () => {
          this.snackBar.open('Password reset successfully', 'Close', { duration: 5000 });
          this.router.navigate(['/login']);
        },
        error: (error: HttpErrorResponse) => {
          this.snackBar.open(getErrorMessage(error), 'Close', { duration: 5000 });
          console.error(error);
        },
      });
  }
}
