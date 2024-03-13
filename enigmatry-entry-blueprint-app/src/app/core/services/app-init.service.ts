import { Injectable } from '@angular/core';
import { AuthService } from '@app/auth/auth.service';
import { ApplicationInsightsService } from '@services/application-insights.service';

export const initFactory = (service: AppInitService) => () => service.init();

@Injectable({
  providedIn: 'root'
})
export class AppInitService {
  constructor(
    private authService: AuthService,
    private appInsightsService: ApplicationInsightsService) { }

  init = async(): Promise<void> => {
    await this.authService.initialize();
    this.appInsightsService.initialize();
  };
}
