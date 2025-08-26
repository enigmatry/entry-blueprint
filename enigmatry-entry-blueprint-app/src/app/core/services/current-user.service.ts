import { HttpStatusCode } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ProfileClient } from '@api';
import { UserProfile } from '@app/auth/user-profile';
import { BehaviorSubject, Observable, catchError, map, of, tap, throwError } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class CurrentUserService {
	private currentUserSubject = new BehaviorSubject<UserProfile | null>(null);
	private readonly profileClient: ProfileClient = inject(ProfileClient);

	public get currentUser$(): Observable<UserProfile | null> {
		return this.currentUserSubject.asObservable();
	}

	public get currentUser(): UserProfile | null {
		return this.currentUserSubject.value;
	}

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