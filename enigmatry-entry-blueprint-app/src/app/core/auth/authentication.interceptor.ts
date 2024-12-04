/* eslint-disable func-style */
import { HttpErrorResponse, HttpEvent, HttpHandlerFn, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { inject } from '@angular/core';
import { environment } from '@env';
import { catchError, Observable, switchMap } from 'rxjs';
import { AuthService } from './auth.service';

const addAuthorizationHeader = (req: HttpRequest<any>, accessToken: string): HttpRequest<any> =>
    req.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } });

export function authenticationInterceptor(request: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
    const authService = inject(AuthService);

    if (!request.url.startsWith(environment.apiUrl)) {
        return next(request);
    }
    return authService
        .getAccessToken()
        .pipe(
            switchMap(accessToken => next(addAuthorizationHeader(request, accessToken))),
            catchError(async error => {
                // eslint-disable-next-line @typescript-eslint/no-unsafe-enum-comparison
                if (error instanceof HttpErrorResponse && error.status === HttpStatusCode.Unauthorized) {
                    await authService.loginRedirect();
                }

                throw error;
            })
        );
}