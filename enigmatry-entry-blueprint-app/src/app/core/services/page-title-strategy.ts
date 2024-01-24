import { Injectable } from '@angular/core';
import { DefaultTitleStrategy, RouterStateSnapshot } from '@angular/router';

/**
 * Overrides default title strategy and sets page title in format 'routeTitle | appTitle'
 * - https://angular.io/api/router/TitleStrategy
 */
@Injectable()
export class PageTitleStrategy extends DefaultTitleStrategy {
  readonly appTitle = $localize`:@@app.title:Entry Blueprint App`;

  override buildTitle(snapshot: RouterStateSnapshot): string {
    const routeTitle = super.buildTitle(snapshot);
    return routeTitle ? `${routeTitle} | ${this.appTitle}` : this.appTitle;
  }
}