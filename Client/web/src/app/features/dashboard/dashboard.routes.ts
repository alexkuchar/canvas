import { Routes } from '@angular/router';
import { Overview } from './overview/overview';
import { authGuard } from '../../core/auth/auth.guard';

export const dashboardRoutes: Routes = [
  {
    path: 'dashboard',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        component: Overview,
      },
    ],
  },
];
