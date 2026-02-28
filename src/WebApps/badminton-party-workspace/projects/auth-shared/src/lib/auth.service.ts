import { Injectable, signal, computed } from '@angular/core';
import liff from '@line/liff';

export interface LiffProfile {
    userId: string;
    displayName: string;
    pictureUrl?: string;
    statusMessage?: string;
    email?: string;
}

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private _isLoggedIn = signal<boolean>(false);
    private _profile = signal<LiffProfile | null>(null);
    private _isInitialized = signal<boolean>(false);

    isLoggedIn = computed(() => this._isLoggedIn());
    profile = computed(() => this._profile());
    isInitialized = computed(() => this._isInitialized());

    /**
     * Initialize LIFF
     * @param liffId The LIFF ID from LINE Developers Console
     */
    async init(liffId: string): Promise<boolean> {
        if (this._isInitialized()) {
            return this._isLoggedIn();
        }

        try {
            await liff.init({ liffId });
            this._isInitialized.set(true);

            if (liff.isLoggedIn()) {
                this._isLoggedIn.set(true);
                const profile = await liff.getProfile();
                this._profile.set(profile as LiffProfile);
                return true;
            }

            this._isLoggedIn.set(false);
            this._profile.set(null);
            return false;
        } catch (err) {
            console.error('LIFF initialization failed', err);
            this._isInitialized.set(false);
            return false;
        }
    }

    /**
     * Trigger LINE login
     * @param redirectUri Optional redirect URI after login
     */
    login(redirectUri?: string) {
        if (!this._isInitialized()) {
            console.error('AuthService must be initialized before login');
            return;
        }

        if (!liff.isLoggedIn()) {
            liff.login({ redirectUri });
        }
    }

    /**
     * Logout from LIFF
     */
    logout() {
        if (liff.isLoggedIn()) {
            liff.logout();
            this._isLoggedIn.set(false);
            this._profile.set(null);
            // Optional: window.location.reload() to clear any state
        }
    }

    /**
     * Get the ID Token for backend verification
     */
    getIdToken(): string | null {
        if (this._isLoggedIn()) {
            return liff.getIDToken();
        }
        return null;
    }
}
