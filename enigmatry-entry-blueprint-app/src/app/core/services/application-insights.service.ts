import { Injectable, OnDestroy } from '@angular/core';
import { NavigationEnd, Router, TitleStrategy } from '@angular/router';
import { environment } from '@env';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { Subject, filter, map, takeUntil } from 'rxjs';
import { CurrentUserService } from './current-user.service';

/**
 * Provides integration with Azure Application Insights
 * - https://github.com/microsoft/ApplicationInsights-JS
 */
@Injectable({
  providedIn: 'root'
})
export class ApplicationInsightsService implements OnDestroy {
  private appInsights: ApplicationInsights | undefined;
  private destroy$ = new Subject<void>();

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

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
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
        connectionString: environment.applicationInsights.connectionString,
        enableCorsCorrelation: environment.applicationInsights.enableCorsCorrelation
      }
    });
  }

  private trackPageViewsOnRouterNavigation() {
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd),
        map(event => event as NavigationEnd),
        takeUntil(this.destroy$)
      )
      .subscribe(event => {
        const title = this.titleStrategy.buildTitle(this.router.routerState.snapshot);
        this.appInsights?.trackPageView({ name: title, uri: event.url });
      });
  }

  private setAuthenticatedUserContext(): void {
    this.currentUserService.currentUser$
      .pipe(
        takeUntil(this.destroy$)
      )
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
