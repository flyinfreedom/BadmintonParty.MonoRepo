import { APP_INITIALIZER, Provider } from '@angular/core';
import { AuthService, AUTH_API_URL } from './auth.service';

export interface AuthConfig {
    liffId: string;
    apiUrl?: string;
}

export function provideLineAuth(config: AuthConfig): Provider[] {
    return [
        {
            provide: AUTH_API_URL,
            useValue: config.apiUrl || 'http://localhost:5263'
        },
        {
            provide: APP_INITIALIZER,
            useFactory: (authService: AuthService) => () => {
                // Initialize LIFF and check status on app startup
                return authService.init(config.liffId);
            },
            deps: [AuthService],
            multi: true,
        },
    ];
}
