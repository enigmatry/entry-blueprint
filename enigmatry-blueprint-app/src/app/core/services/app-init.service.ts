import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';

export const initFactory = (config: AppInitService) => () => config.init();

@Injectable({
    providedIn: 'root'
})
export class AppInitService {
    constructor(private authService: AuthService) { }

    init = async(): Promise<void> => {
        await this.authService.handleAuthCallback();
    };
}
