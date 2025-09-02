import { Routes } from '@angular/router';
import { RouteSegments } from '@shared/model/route-segments';

export const userRoutes: Routes = [
  {
    path: `${RouteSegments.users}/${RouteSegments.edit}/:id`,
    loadComponent: () => import('./user-edit/user-edit.component').then(module => module.UserEditComponent),
    title: $localize`:@@route.users.edit:Update user`
  }
];
