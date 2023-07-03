import { Inject, Injectable } from '@angular/core';

import { from, Observable, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { extraQueryParameters, MSAL_CONFIG } from './msal-config';
import {
  AccountInfo, AuthenticationResult, Configuration, PublicClientApplication, RedirectRequest
} from '@azure/msal-browser';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MsalAuthService {
  private msalInstance: PublicClientApplication;

  constructor(@Inject(MSAL_CONFIG) msalConfig: Configuration) {
    this.msalInstance = new PublicClientApplication(msalConfig);
  }

  handleAuthCallback(): Promise<AuthenticationResult | null> {
    return this.msalInstance.handleRedirectPromise();
  }

  loginRedirect(email?: string): void {
    const loginRequest: RedirectRequest = {
      scopes: environment.azureAdB2C.scopes,
      loginHint: email,
      extraQueryParameters
    };
    this.msalInstance.loginRedirect(loginRequest);
  }

  isAuthenticated(): boolean {
    const account = this.getAccount();
    return !!account;
  }

  getAccount(): AccountInfo {
    return this.msalInstance.getActiveAccount() || this.msalInstance.getAllAccounts()?.[0];
  }

  logout() {
    this.msalInstance
      .logoutRedirect()
      .catch(_ => {
        this.msalInstance.setActiveAccount(null);
      });
  }

  getAccessToken(): Observable<string> {
    return this.acquireTokenSilent(this.getAccount())
      .pipe(
        switchMap((result: AuthenticationResult) => {
          if (!result.accessToken) {
            return this.acquireTokenRedirect();
          }
          return of(result);
        }),
        catchError(() => this.acquireTokenRedirect()),
        map((result: AuthenticationResult | void) => result instanceof Object ? result.accessToken : '')
      );
  }

  private acquireTokenSilent(accountInfo: AccountInfo): Observable<AuthenticationResult> {
    const silentRequest = {
      account: accountInfo,
      scopes: environment.azureAdB2C.scopes
    };
    return from(this.msalInstance.acquireTokenSilent(silentRequest));
  }

  private acquireTokenRedirect(): Observable<void> {
    const interactiveRequest = {
      scopes: environment.azureAdB2C.scopes,
      extraQueryParameters
    };
    return from(this.msalInstance.acquireTokenRedirect(interactiveRequest));
  }
}
