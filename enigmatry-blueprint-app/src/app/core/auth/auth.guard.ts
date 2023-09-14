import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { map, tap } from 'rxjs';
import { CurrentUserService } from '../services/current-user.service';
import { AuthService } from './auth.service';

export const authGuard: CanActivateFn = (
  _route: ActivatedRouteSnapshot,
  _state: RouterStateSnapshot
) => {
  const authService = inject(AuthService);
  const currentUserService = inject(CurrentUserService);

  const isAuthenticated = authService.isAuthenticated();
  if (!isAuthenticated) {
    authService.loginRedirect();
    return false;
  }

  return currentUserService.getUser()
    .pipe(
      tap(user => {
        if (!user) {
          authService.logout();
        }
      }),
      map(user => !!user)
    );
};
