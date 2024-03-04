import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpStatusCode
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  intercept = (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> => {
    if (!req.url.startsWith(environment.apiUrl)) {
      return next.handle(req);
    }
    return this.authService
      .getAccessToken()
      .pipe(
        switchMap(accessToken => next.handle(this.addAuthorizationHeader(req, accessToken))),
        catchError(async error => {
          // eslint-disable-next-line @typescript-eslint/no-unsafe-enum-comparison
          if (error instanceof HttpErrorResponse && error.status === HttpStatusCode.Unauthorized) {
            await this.authService.loginRedirect();
          }

          throw error;
        })
      );
  };

  private addAuthorizationHeader = (req: HttpRequest<any>, accessToken: string): HttpRequest<any> =>
    req.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } });
}
