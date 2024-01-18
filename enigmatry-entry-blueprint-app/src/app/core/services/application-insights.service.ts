import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AngularPlugin } from '@microsoft/applicationinsights-angularplugin-js';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { environment } from 'src/environments/environment';
import { CurrentUserService } from './current-user.service';

/**
 * Provides Azure Application Insights integration
 *
 * - https://github.com/microsoft/applicationinsights-angularplugin-js
 * - https://timdeschryver.dev/blog/configuring-azure-application-insights-in-an-angular-application
 */
@Injectable({
  providedIn: 'root'
})
export class ApplicationInsightsService {
  private appInsights: ApplicationInsights | undefined;

  constructor(private router: Router, private currentUserService: CurrentUserService) { }

  initialize(): void {
    if (!environment.applicationInsights.connectionString) {
      // Skip initialization when connection string is not set
      return;
    }
    try {
      this.appInsights = this.createApplicationInsights();
      this.appInsights.loadAppInsights();

      this.appInsights.context.application.ver = environment.appVersion;
      this.setAuthenticatedUserContext();
    } catch (error) {
      // eslint-disable-next-line no-console
      console.error('Error loading AppInsights:', error);
    }
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
    const angularPlugin = new AngularPlugin();

    return new ApplicationInsights({
      config: {
        connectionString: environment.applicationInsights.connectionString,
        extensions: [angularPlugin],
        extensionConfig: {
          [angularPlugin.identifier]: {
            // Set router to enable page view tracking
            router: this.router
          }
        }
      }
    });
  }

  private setAuthenticatedUserContext(): void {
    this.currentUserService.currentUser$
      .subscribe(currentUser => {
        const userId = currentUser?.id;
        if (userId) {
          this.appInsights?.setAuthenticatedUserContext(userId, userId, false);
        }
      });
  }
}
