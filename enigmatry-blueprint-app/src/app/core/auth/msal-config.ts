/* eslint-disable func-style */
import { InjectionToken } from '@angular/core';
import { Configuration, LogLevel } from '@azure/msal-browser';
import { environment } from 'src/environments/environment';

export const MSAL_CONFIG = new InjectionToken<string>('MSAL_CONFIG');

// eslint-disable-next-line camelcase
export const extraQueryParameters = { ui_locales: 'nl' };

export function msalLoggerCallback(_: LogLevel, message: string) {
  if (!environment.production) {
    // eslint-disable-next-line no-console
    console.log(message);
  }
}

export function msalConfigFactory(): Configuration {
  return {
    auth: {
      clientId: environment.azureAdB2C.clientId,
      // eslint-disable-next-line max-len
      authority: `${environment.azureAdB2C.instance}/${environment.azureAdB2C.domain}/${environment.azureAdB2C.signUpSignInPolicyId}`,
      knownAuthorities: [environment.azureAdB2C.instance],
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