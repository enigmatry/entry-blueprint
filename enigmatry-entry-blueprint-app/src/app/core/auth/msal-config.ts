/* eslint-disable func-style */
import { InjectionToken } from '@angular/core';
import { Configuration, LogLevel } from '@azure/msal-browser';
import { environment } from 'src/environments/environment';
import { getCurrentLanguage } from 'src/i18n/language';

export const MSAL_CONFIG = new InjectionToken<string>('MSAL_CONFIG');

// eslint-disable-next-line camelcase
export const extraQueryParameters = { ui_locales: getCurrentLanguage() };

export function msalLoggerCallback(_: LogLevel, message: string) {
  if (!environment.production) {
    // eslint-disable-next-line no-console
    console.log(message);
  }
}

export function msalConfigFactory(): Configuration {
  const authorityDomain = new URL(environment.azureAd.authority).hostname;
  return {
    auth: {
      clientId: environment.azureAd.clientId,
      authority: environment.azureAd.authority,
      knownAuthorities: [authorityDomain],
      redirectUri: `${location.protocol}//${location.host}`,
      navigateToLoginRequestUrl: true
    },
    cache: {
      cacheLocation: 'sessionStorage'
    },
    system: {
      loggerOptions: {
        loggerCallback: msalLoggerCallback,
        piiLoggingEnabled: !environment.production
      }
    }
  };
}