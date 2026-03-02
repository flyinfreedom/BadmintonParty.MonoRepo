import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from 'auth-shared';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        RouterModule,
        MatCardModule,
        MatButtonModule,
        MatIconModule
    ],
    templateUrl: './login.html',
    styleUrls: ['./login.scss']
})
export class LoginComponent {
    constructor(private auth: AuthService, private router: Router) {
        // If already logged in, redirect to admin
        if (this.auth.isLoggedIn()) {
            this.router.navigate(['/admin']);
        }
    }

    loginWithLine() {
        this.auth.login(window.location.origin + '/admin');
    }
}
