import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from 'auth-shared';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  constructor(protected auth: AuthService) { }

  protected readonly title = signal('court-management');

  onLogin() {
    this.auth.login();
  }

  onLogout() {
    this.auth.logout();
  }
}
