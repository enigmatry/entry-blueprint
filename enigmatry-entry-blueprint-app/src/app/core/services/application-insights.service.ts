import { Injectable } from '@angular/core';
import { NavigationEnd, Router, TitleStrategy } from '@angular/router';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { filter, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CurrentUserService } from './current-user.service';

/**
 * Provides integration with Azure Application Insights
 * - https://github.com/microsoft/ApplicationInsights-JS
 */
@Injectable({
  providedIn: 'root'
})
export class ApplicationInsightsService {
  private appInsights: ApplicationInsights | undefined;

  constructor(private router: Router, private titleStrategy: TitleStrategy,
    private currentUserService: CurrentUserService) { }

  initialize(): void {
    if (!environment.applicationInsights.connectionString) {
      // Skip initialization when connection string is not set
      return;
    }

    this.appInsights = this.createApplicationInsights();
    this.appInsights.loadAppInsights();

    this.appInsights.context.application.ver = environment.appVersion;
    this.setAuthenticatedUserContext();

    this.trackPageViewsOnRouterNavigation();
  }

  trackEvent(name: string, properties?: { [key: string]: any }) {
    this.appInsights?.trackEvent({ name }, properties);
  }

  trackException(exception: Error, properties?: { [key: string]: any }) {
    this.appInsights?.trackException({ exception, properties });
  }

  trackTrace(message: string, properties?: { [key: string]: any }) {
    this.appInsights?.trackTrace({ message }, properties);
  }

  private createApplicationInsights(): ApplicationInsights {
    return new ApplicationInsights({
      config: {
        connectionString: environment.applicationInsights.connectionString
      }
    });
  }

  private trackPageViewsOnRouterNavigation() {
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd),
        map(event => event as NavigationEnd)
      )
      .subscribe(event => {
        const title = this.titleStrategy.buildTitle(this.router.routerState.snapshot);
        this.appInsights?.trackPageView({ name: title, uri: event.url });
      });
  }

  private setAuthenticatedUserContext(): void {
    this.currentUserService.currentUser$
      .subscribe(currentUser => {
        const userId = currentUser?.id;
        if (userId) {
          this.appInsights?.setAuthenticatedUserContext(userId);
        } else {
          this.appInsights?.clearAuthenticatedUserContext();
        }
      });
  }
}
