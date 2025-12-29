import { Component, computed, effect, inject, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule, FormGroup, Validators } from '@angular/forms';
import { MatFormField, MatLabel, MatError } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatInput } from '@angular/material/input';
import { AuthService } from '../../../core/auth/auth.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { UserService } from '../../../core/user/user.service';
import { UpdateUserRequest } from '../../../core/user/user.types';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';
import { getErrorMessage } from '../../../core/http-error.util';

@Component({
  selector: 'app-settings',
  imports: [
    ReactiveFormsModule,
    MatFormField,
    MatLabel,
    MatIcon,
    MatError,
    MatIconButton,
    MatInput,
    MatButton,
  ],
  templateUrl: './settings.html',
  styleUrls: ['./settings.scss'],
})
export class Settings {
  private readonly authService = inject(AuthService);
  private readonly userService = inject(UserService);
  readonly user = toSignal(this.authService.user$, { initialValue: this.authService.user });
  private readonly snackBar = inject(MatSnackBar);

  readonly userFirstName = computed(() => this.user()?.user.firstName ?? '');
  readonly userLastName = computed(() => this.user()?.user.lastName ?? '');
  readonly userEmail = computed(() => this.user()?.user.email ?? '');
  hide = signal(true);

  settingsForm = new FormGroup({
    firstName: new FormControl('', [Validators.minLength(2)]),
    lastName: new FormControl('', [Validators.minLength(2)]),
    email: new FormControl('', [Validators.email]),
    password: new FormControl('', [Validators.minLength(8)]),
  });

  constructor() {
    effect(() => {
      const firstName = this.userFirstName();
      const lastName = this.userLastName();
      const email = this.userEmail();

      if (firstName || lastName || email) {
        this.settingsForm.patchValue({
          firstName,
          lastName,
          email,
        });
      }
    });
  }

  onSubmit() {
    if (this.settingsForm.invalid) {
      return;
    }

    if (!this.user()?.user.id) {
      this.authService.logout();
      return;
    }

    this.userService
      .updateUser({
        id: this.user()?.user.id,
        firstName: this.settingsForm.get('firstName')?.value,
        lastName: this.settingsForm.get('lastName')?.value,
        email: this.settingsForm.get('email')?.value,
        password: this.settingsForm.get('password')?.value,
      } as UpdateUserRequest)
      .subscribe({
        next: () => {
          this.snackBar.open('User updated successfully', 'Close', { duration: 5000 });
        },
        error: (error: HttpErrorResponse) => {
          this.snackBar.open(getErrorMessage(error), 'Close', { duration: 5000 });
          console.error(error);
        },
      });
  }
}
