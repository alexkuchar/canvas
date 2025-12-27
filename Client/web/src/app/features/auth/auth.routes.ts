import { Routes } from '@angular/router';
import { Register } from './register/register';
import { Login } from './login/login';
import { ForgotPassword } from './forgot-password/forgot-password';
import { ResetPassword } from './reset-password/reset-password';
import { Verify } from './verify/verify';

export const authRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'register',
        component: Register,
      },
      {
        path: 'login',
        component: Login,
      },
      {
        path: 'forgot-password',
        component: ForgotPassword,
      },
      {
        path: 'reset-password',
        component: ResetPassword,
      },
      {
        path: 'verify',
        component: Verify,
      },
    ],
  },
];
