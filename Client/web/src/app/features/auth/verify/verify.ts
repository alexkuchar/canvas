import { Component, inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { getErrorMessage } from '../../../core/http-error.util';

@Component({
  selector: 'app-verify',
  imports: [],
  templateUrl: './verify.html',
})
export class Verify implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly route = inject(ActivatedRoute);
  private readonly snackBar = inject(MatSnackBar);
  private readonly router = inject(Router);

  private readonly token = (() => {
    const token = this.route.snapshot.queryParamMap.get('token');
    if (!token) {
      this.snackBar.open('Invalid token', 'Close', { duration: 5000 });
      return null;
    }
    return token;
  })();

  ngOnInit() {
    if (!this.token) {
      return;
    }

    this.authService.verifyUser({ verificationToken: encodeURIComponent(this.token) }).subscribe({
      next: () => {
        this.snackBar.open('User verified successfully', 'Close', { duration: 5000 });
        this.router.navigate(['/login']);
      },
      error: (error: HttpErrorResponse) => {
        this.snackBar.open(getErrorMessage(error), 'Close', { duration: 5000 });
        console.error(error);
      },
    });
  }
}
