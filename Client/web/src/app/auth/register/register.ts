import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import {
  MatCard,
  MatCardHeader,
  MatCardTitle,
  MatCardSubtitle,
  MatCardContent,
} from '@angular/material/card';
import { MatFormField, MatLabel, MatError, MatSuffix } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatIcon } from '@angular/material/icon';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../core/auth/auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { getErrorMessage } from '../../core/http-error.util';

@Component({
  selector: 'app-register',
  imports: [
    CommonModule,
    MatCard,
    MatCardHeader,
    MatCardTitle,
    MatCardSubtitle,
    MatCardContent,
    MatFormField,
    MatLabel,
    MatInput,
    ReactiveFormsModule,
    MatError,
    MatSuffix,
    MatIcon,
    MatIconButton,
    MatButton,
  ],
  templateUrl: './register.html',
  styleUrls: ['./register.scss'],
})
export class Register {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  hide = signal(true);
  private readonly snackBar = inject(MatSnackBar);

  registerForm = new FormGroup({
    fullName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      this.minWordsValidator(2),
    ]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
  });

  private minWordsValidator(minWords: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value) {
        return null;
      }
      const words = control.value
        .trim()
        .split(/\s+/)
        .filter((word: string) => word.length > 0);
      return words.length >= minWords
        ? null
        : { minWords: { required: minWords, actual: words.length } };
    };
  }

  onSubmit() {
    if (this.registerForm.invalid) {
      return;
    }

    this.authService
      .register({
        firstName: this.registerForm.value.fullName?.split(' ')[0] ?? '',
        lastName: this.registerForm.value.fullName?.split(' ')[1] ?? '',
        email: this.registerForm.value.email ?? '',
        password: this.registerForm.value.password ?? '',
      })
      .subscribe({
        next: () => {
          this.snackBar.open('Please check your email for verification', 'Close', {
            duration: 5000,
          });
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
