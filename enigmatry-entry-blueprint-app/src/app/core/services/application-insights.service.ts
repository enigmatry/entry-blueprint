import { effect, inject, Injectable, OnDestroy } from '@angular/core';
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
  private readonly router: Router = inject(Router);
  private readonly titleStrategy: TitleStrategy = inject(TitleStrategy);
  private readonly currentUserService: CurrentUserService = inject(CurrentUserService);

  constructor() {
    effect(() => {
      const userId = this.currentUserService.currentUser()?.id;
      if (userId) {
        this.appInsights?.setAuthenticatedUserContext(userId);
      } else {
        this.appInsights?.clearAuthenticatedUserContext();
      }
    });
  }

  initialize(): void {
    if (!environment.applicationInsights.connectionString) {
      // Skip initialization when connection string is not set
      return;
    }

    this.appInsights = this.createApplicationInsights();
    this.appInsights.loadAppInsights();

    this.appInsights.context.application.ver = environment.appVersion;
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

  createApplicationInsights = (): ApplicationInsights => new ApplicationInsights({
    config: {
      connectionString: environment.applicationInsights.connectionString,
      enableCorsCorrelation: environment.applicationInsights.enableCorsCorrelation
    }
  });

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
}
