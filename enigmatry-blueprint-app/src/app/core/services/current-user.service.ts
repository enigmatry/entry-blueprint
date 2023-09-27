import { HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, of, tap, throwError } from 'rxjs';
import { ProfileClient } from 'src/app/api/api-reference';
import { UserProfile } from '../auth/user-profile';

@Injectable({
	providedIn: 'root'
})
export class CurrentUserService {
	private currentUserSubject = new BehaviorSubject<UserProfile | null>(null);

	public get currentUser(): UserProfile | null {
		return this.currentUserSubject.value;
	}

	constructor(private profileClient: ProfileClient) { }

	loadUser(): Observable<UserProfile | null> {
		if (this.currentUserSubject.value) {
			return this.currentUserSubject.asObservable();
		}
		return this.profileClient.getProfile()
			.pipe(
				map(response => UserProfile.fromResponse(response)),
				tap(profile => this.currentUserSubject.next(profile)),
				catchError(error => {
					if (error.status === HttpStatusCode.NotFound) {
						return of(null);
					}
					return throwError(() => error);
				})
			);
	}
}