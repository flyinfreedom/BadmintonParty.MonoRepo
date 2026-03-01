import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { AuthService } from 'auth-shared';
import { Router } from '@angular/router';

@Component({
    selector: 'app-admin-layout',
    standalone: true,
    imports: [
        CommonModule,
        RouterModule,
        MatSidenavModule,
        MatToolbarModule,
        MatListModule,
        MatIconModule,
        MatButtonModule,
        MatMenuModule
    ],
    templateUrl: './admin-layout.html',
    styleUrls: ['./admin-layout.scss']
})
export class AdminLayoutComponent {
    auth = inject(AuthService);
    router = inject(Router);

    isSidenavOpen = true;

    menuItems = [
        { icon: 'dashboard', label: 'Dashboard', route: '/admin/dashboard' },
        { icon: 'business', label: '羽球館管理', route: '/admin/centers' },
        { icon: 'stadium', label: '羽球場管理', route: '/admin/courts' },
        { icon: 'calendar_today', label: 'Court Booking', route: '/admin/bookings' },
        { icon: 'people', label: 'Members', route: '/admin/members' },
        { icon: 'payments', label: 'Payments', route: '/admin/payments' },
        { icon: 'settings', label: 'Settings', route: '/admin/settings' }
    ];

    toggleSidenav() {
        this.isSidenavOpen = !this.isSidenavOpen;
    }

    logout() {
        this.auth.logout();
        this.router.navigate(['/']);
    }
}
