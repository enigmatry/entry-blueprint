import { inject, Injector, runInInjectionContext } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { entryPermissionGuard } from '@enigmatry/entry-components';
import { CurrentUserService } from '@services/current-user.service';
import { AuthService } from './auth.service';

export const authGuard = async(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const authService = inject(AuthService);
  const currentUserService = inject(CurrentUserService);
  const injector = inject(Injector);
  const isAuthenticated = authService.isAuthenticated();

  if (!isAuthenticated) {
    await authService.loginRedirect({ redirectStartPage: state.url });
    return false;
  }

  await currentUserService.loadUser();
  const user = currentUserService.currentUser();

  if (!user) {
    authService.logout();
    return false;
  }

  // If no permission is required, allow authenticated user to pass.
  if(!route.data['permissions']) {
    return true;
  }

  // eslint-disable-next-line @typescript-eslint/return-await
  return runInInjectionContext(injector, async() => await entryPermissionGuard(route, state));
};