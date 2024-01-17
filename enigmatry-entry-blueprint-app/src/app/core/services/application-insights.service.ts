import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AngularPlugin } from '@microsoft/applicationinsights-angularplugin-js';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { environment } from 'src/environments/environment';

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

  constructor(private router: Router) { }

  /**
   * Performs initialization logic
   */
  initialize(): void {
    if (!environment.applicationInsights.connectionString) {
      // Skip initialization when connection string is not set
      return;
    }
    try {
      this.appInsights = this.createApplicationInsights();
      this.appInsights.loadAppInsights();
    } catch (error) {
      // eslint-disable-next-line no-console
      console.error(`Error loading AppInsights: ${error}`);
    }
  }

  /**
   * Manually log page view
   * @param name
   * @param uri
   */
  trackPageView(name?: string, uri?: string) {
    this.appInsights?.trackPageView({ name, uri });
  }

  /**
   * Log custom event
   * @param name
   * @param properties
   */
  trackEvent(name: string, properties?: { [key: string]: any }) {
    this.appInsights?.trackEvent({ name }, properties);
  }

  /**
   * Log metric value
   * @param name
   * @param average
   * @param properties
   */
  trackMetric(name: string, average: number, properties?: { [key: string]: any }) {
    this.appInsights?.trackMetric({ name, average }, properties);
  }

  /**
   * Manually log an exception
   * @param exception
   * @param properties
   */
  trackException(exception: Error, properties?: { [key: string]: any }) {
    this.appInsights?.trackException({ exception, properties });
  }

  /**
   * Log trace message
   * @param message
   * @param properties
   */
  trackTrace(message: string, properties?: { [key: string]: any }) {
    this.appInsights?.trackTrace({ message }, properties);
  }

  /**
   * Set up an instance of ApplicationInsights with AngularPlugin and Router
   * @returns {ApplicationInsights}
   */
  private createApplicationInsights(): ApplicationInsights {
    const angularPlugin = new AngularPlugin();

    return new ApplicationInsights({
      config: {
        connectionString: environment.applicationInsights.connectionString,
        extensions: [angularPlugin],
        extensionConfig: {
          [angularPlugin.identifier]: {
            // Router for enabling page view tracking
            router: this.router
          }
        }
      }
    });
  }
}
