import { Routes } from '@angular/router';
import { LandingPageComponent } from './pages/landing/landing.page';
import { LoginComponent } from './pages/login/login';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout';
import { DashboardComponent } from './pages/admin/dashboard/dashboard';

export const routes: Routes = [
    { path: '', component: LandingPageComponent },
    { path: 'login', component: LoginComponent },
    {
        path: 'admin',
        component: AdminLayoutComponent,
        children: [
            { path: 'dashboard', component: DashboardComponent },
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
        ]
    },
    { path: '**', redirectTo: '' }
];
