/* eslint-disable max-len */
/* eslint-disable func-style */
import { Configuration, LogLevel } from '@azure/msal-browser';
import { InjectionToken } from '@angular/core';
import { environment } from 'src/environments/environment';

export const MSAL_CONFIG = new InjectionToken<string>('MSAL_CONFIG');

export const extraQueryParameters = { ui_locales: 'nl' };

function isIEOrEdge(): boolean {
  const ua = window.navigator.userAgent;
  const msie = ua.indexOf('MSIE ');
  const msie11 = ua.indexOf('Trident/');
  const msedge = ua.indexOf('Edge/');
  const isIE = msie > 0 || msie11 > 0;
  const isEdge = msedge > 0;
  return isIE || isEdge;
}

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
      authority: `${environment.azureAdB2C.instance}/${environment.azureAdB2C.domain}/${environment.azureAdB2C.signUpSignInPolicyId}`,
      knownAuthorities: [environment.azureAdB2C.instance],
      redirectUri: `${location.protocol}//${location.host}`,
      navigateToLoginRequestUrl: true
    },
    cache: {
      cacheLocation: 'sessionStorage',
      storeAuthStateInCookie: isIEOrEdge()
    },
    system: {
      loggerOptions: {
        loggerCallback: msalLoggerCallback,
        piiLoggingEnabled: !environment.production
      }
    }
  };
}
