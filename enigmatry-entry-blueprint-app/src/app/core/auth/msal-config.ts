import { InjectionToken } from '@angular/core';
import { Configuration, LogLevel } from '@azure/msal-browser';
import { environment } from '@env';
import { getCurrentLanguage } from 'src/i18n/language';

const msalLoggerCallback = (_: LogLevel, message: string) => {
  if (!environment.production) {
    // eslint-disable-next-line no-console
    console.log(message);
  }
};

const msalConfigFactory = (): Configuration => {
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
};

export const extraQueryParameters = {
  // eslint-disable-next-line camelcase
  ui_locales: getCurrentLanguage()
};

export const MSAL_CONFIG = new InjectionToken<Configuration>('msal_config', {
  providedIn: 'root',
  factory: () => msalConfigFactory()
});