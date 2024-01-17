import { Injectable, ErrorHandler } from '@angular/core';
import { ApplicationInsightsService } from './services/application-insights.service';

/**
 * Extend the default error handling to report errors to an external service - e.g Azure Application Insights.
 */
@Injectable()
export class GlobalErrorHandler extends ErrorHandler {
  constructor(private _appInsightsService: ApplicationInsightsService) {
    super();
  }

  override handleError(error: any) {
    // Default error handler prints error messages to the console
    super.handleError(error);

    // Log error to application insights
    this._appInsightsService.trackException(error);
  }
}