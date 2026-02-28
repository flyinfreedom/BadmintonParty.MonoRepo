import { APP_INITIALIZER, Provider } from '@angular/core';
import { AuthService } from './auth.service';

export interface AuthConfig {
    liffId: string;
}

export function provideLineAuth(config: AuthConfig): Provider[] {
    return [
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
