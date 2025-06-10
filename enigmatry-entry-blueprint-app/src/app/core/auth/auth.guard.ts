import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { CurrentUserService } from '@services/current-user.service';
import { from, map, tap } from 'rxjs';
import { AuthService } from './auth.service';

export const authGuard: CanActivateFn = (_route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const authService = inject(AuthService);
  const currentUserService = inject(CurrentUserService);

  const isAuthenticated = authService.isAuthenticated();

  if (!isAuthenticated) {
    return from(authService.loginRedirect({ redirectStartPage: state.url }))
      .pipe(map(_ => false));
  }

  return currentUserService.loadUser()
    .pipe(
      tap(user => {
        if (!user) {
          authService.logout();
        }
      }),
      map(user => !!user)
    );
};