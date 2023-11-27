import { Inject, Injectable } from '@angular/core';

import {
  AccountInfo, AuthenticationResult, Configuration, PublicClientApplication, RedirectRequest
} from '@azure/msal-browser';
import { from, Observable, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { extraQueryParameters, MSAL_CONFIG } from './msal-config';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private msalInstance: PublicClientApplication;

  constructor(@Inject(MSAL_CONFIG) private msalConfig: Configuration) {
  }

  async initialize(): Promise<void> {
    if (!this.msalInstance) {
      this.msalInstance = new PublicClientApplication(this.msalConfig);
      await this.msalInstance.initialize();
    }
    await this.msalInstance.handleRedirectPromise()
      // eslint-disable-next-line no-console
      .catch(error => console.warn(error));
  }

  loginRedirect(request?: Partial<RedirectRequest>): Promise<void> {
    const loginRequest: RedirectRequest = {
      scopes: environment.azureAd.scopes,
      extraQueryParameters,
      ...request
    };
    return this.msalInstance.loginRedirect(loginRequest);
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
        map((result: AuthenticationResult | void) => result?.accessToken ?? '')
      );
  }

  private acquireTokenSilent(accountInfo: AccountInfo): Observable<AuthenticationResult> {
    const silentRequest = {
      account: accountInfo,
      scopes: environment.azureAd.scopes
    };
    return from(this.msalInstance.acquireTokenSilent(silentRequest));
  }

  private acquireTokenRedirect(): Observable<void> {
    const interactiveRequest = {
      scopes: environment.azureAd.scopes,
      extraQueryParameters
    };
    return from(this.msalInstance.acquireTokenRedirect(interactiveRequest));
  }
}
