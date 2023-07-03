import { Injectable } from '@angular/core';
import { MsalAuthService } from './core/auth/azure-ad-b2c/msal-auth.service';

export const initFactory = (config: AppInitService) => () => config.init();

@Injectable({
    providedIn: 'root'
})
export class AppInitService {
    constructor(private authService: MsalAuthService) { }

    init = async(): Promise<void> => {
        await this.authService.handleAuthCallback();
    };
}
