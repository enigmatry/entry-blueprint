import { ErrorHandler, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AngularPlugin } from '@microsoft/applicationinsights-angularplugin-js';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { environment } from 'src/environments/environment';

/**
 * Provides Azure Application Insights integration
 *
 * References:
 * - https://github.com/microsoft/applicationinsights-angularplugin-js
 * - https://timdeschryver.dev/blog/configuring-azure-application-insights-in-an-angular-application
 */
@Injectable({
  providedIn: 'root'
})
export class ApplicationInsightsService {
  private angularPlugin = new AngularPlugin();

  private appInsights = new ApplicationInsights({
    config: {
      connectionString: environment.applicationInsights.connectionString,
      extensions: [this.angularPlugin],
      extensionConfig: {
        [this.angularPlugin.identifier]: {
          router: this.router,
          errorServices: [new ErrorHandler()]
        }
      }
    }
  });

  constructor(private router: Router) {
    this.appInsights.loadAppInsights();
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
}
