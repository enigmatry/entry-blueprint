import { HttpStatusCode } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { ProfileClient } from '@api';
import { UserProfile } from '@app/auth/user-profile';
import { lastValueFrom } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class CurrentUserService {
	readonly currentUser = signal<UserProfile | null>(null);
	private readonly profileClient: ProfileClient = inject(ProfileClient);

	readonly loadUser = async() => {
		if (this.currentUser()) {
			return;
		}

		try {
			const response = await lastValueFrom(this.profileClient.getProfile());
			const profile = UserProfile.fromResponse(response);
			this.currentUser.set(profile);
		} catch(error) {
			if (error as Record<string, unknown>['status'] === HttpStatusCode.NotFound) {
				this.currentUser.set(null);
				return;
			}
			throw error;
		}
	};
}