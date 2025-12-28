import { Routes } from '@angular/router';
import { Overview } from './overview/overview';
import { authGuard } from '../../core/auth/auth.guard';
import { Layout } from './layout/layout';
import { Settings } from './settings/settings';

export const dashboardRoutes: Routes = [
  {
    path: 'dashboard',
    canActivate: [authGuard],
    component: Layout,
    children: [
      {
        path: '',
        component: Overview,
      },
      {
        path: 'settings',
        component: Settings,
      },
    ],
  },
];
