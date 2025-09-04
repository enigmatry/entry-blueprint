import { Routes } from '@angular/router';
import { PermissionId } from '@api';
import { authGuard } from '@app/auth/auth.guard';
import { RouteSegments } from '@shared/model/route-segments';
import { UserEditComponent } from './user-edit/user-edit.component';

export const userRoutes: Routes = [
  {
    path: `${RouteSegments.users}/${RouteSegments.edit}/:id`,
    component: UserEditComponent,
    canActivate: [authGuard],
    data: {
      permissions: {
        only: [PermissionId.UsersWrite]
      }
    },
    title: $localize`:@@route.users.edit:Update user`
  }
];
