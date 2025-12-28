import { Component, computed, effect, inject, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule, FormGroup, Validators } from '@angular/forms';
import { MatFormField, MatLabel, MatError } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatInput } from '@angular/material/input';
import { AuthService } from '../../../core/auth/auth.service';
import { toSignal } from '@angular/core/rxjs-interop';

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
  readonly user = toSignal(this.authService.user$, { initialValue: this.authService.user });
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
    console.log(this.settingsForm.value);
  }
}
