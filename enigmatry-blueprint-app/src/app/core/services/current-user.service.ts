import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, of, tap, throwError } from 'rxjs';
import { GetUserProfileResponse, ProfileClient } from 'src/app/api/api-reference';

@Injectable({
    providedIn: 'root'
})
export class CurrentUserService {
    private currentUserSubject = new BehaviorSubject<GetUserProfileResponse | null>(null);

    constructor(private profileClient: ProfileClient) { }

    getUser(): Observable<GetUserProfileResponse | null> {
        if (this.currentUserSubject.value) {
            return of(this.currentUserSubject.value);
        }
        return this.profileClient.getProfile().pipe(
            tap(response => this.currentUserSubject.next(response)),
            catchError(error => {
                // eslint-disable-next-line @typescript-eslint/no-magic-numbers
                if (error.status === 404) {
                    return of(null);
                }
                return throwError(() => error);
              })
        );
    }
}
