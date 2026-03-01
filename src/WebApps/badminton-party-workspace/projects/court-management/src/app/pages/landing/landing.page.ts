import { Component, effect, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AuthService } from 'auth-shared';

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [CommonModule, RouterModule, MatButtonModule, MatIconModule, MatToolbarModule],
  templateUrl: './landing.page.html',
  styleUrls: ['./landing.page.scss']
})
export class LandingPageComponent {
  private router = inject(Router);

  constructor(protected auth: AuthService) {
    effect(() => {
      if (this.auth.backendToken()) {
        this.router.navigate(['/admin/dashboard']);
      }
    });
  }

  login() {
    this.auth.login();
  }
}
