import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MsalAuthService } from './azure-ad-b2c/msal-auth.service';

export const isApiUrl = (url: string): boolean => url.startsWith(environment.apiUrl);

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: MsalAuthService) {}

  intercept = (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> => {
    if (!isApiUrl(req.url) || !this.authService.isAuthenticated()) {
      return next.handle(req);
    }

    return this.authService.getAccessToken().pipe(
      switchMap((accessToken: string) => {
        let request: HttpRequest<any>;
        if (accessToken) {
          request = req.clone({
            setHeaders: { Authorization: `Bearer ${accessToken}` }
          });
        } else {
          request = req;
        }

        return next.handle(request);
      })
    );
  };
}
