import { Routes } from '@angular/router';
import { Register } from './register/register';

export const authRoutes: Routes = [
    {
        path: 'register',
        component: Register
    }
];