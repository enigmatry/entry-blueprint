import { Injectable, ErrorHandler, inject } from '@angular/core';
import { ApplicationInsightsService } from './application-insights.service';

/**
 * Extends default error handling to report errors to an external service - Azure Application Insights.
 * - https://angular.io/api/core/ErrorHandler
 */
@Injectable()
export class GlobalErrorHandler extends ErrorHandler {
  private readonly appInsightsService = inject(ApplicationInsightsService);

  override handleError(error: any) {
    // Default error handler prints error messages to the console
    super.handleError(error);

    // Log error to application insights
    this.appInsightsService.trackException(error);
  }
}