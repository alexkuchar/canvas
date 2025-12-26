import { Routes } from '@angular/router';
import { Register } from './register/register';

export const authRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'register',
        component: Register,
      },
    ],
  },
];
