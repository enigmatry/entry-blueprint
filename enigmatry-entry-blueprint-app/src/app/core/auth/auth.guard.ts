import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { CurrentUserService } from '@services/current-user.service';
import { AuthService } from './auth.service';

export const authGuard: CanActivateFn = async(_route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const authService = inject(AuthService);
  const currentUserService = inject(CurrentUserService);

  const isAuthenticated = authService.isAuthenticated();

  if (!isAuthenticated) {
    await authService.loginRedirect({ redirectStartPage: state.url });
    return false;
  }

  await currentUserService.loadUser();
  const user = currentUserService.currentUser();

  if (!user) {
    authService.logout();
  }
  return !!user;
};