import { Routes } from '@angular/router';
import { PermissionId } from '@api';
import { authGuard } from '@app/auth/auth.guard';
import { entryPermissionGuard } from '@enigmatry/entry-components/permissions';
import { HomeComponent } from '@features/home/home.component';
import { productRoutes } from '@features/products/product-routes';
import { userRoutes } from '@features/users/user-routes';
import { RouteSegments } from '@shared/model/route-segments';


export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [authGuard],
    title: $localize`:@@route.home:Home`
  },
  {
    path: RouteSegments.users,
    canActivate: [authGuard],
    canActivateChild: [entryPermissionGuard],
    title: $localize`:@@route.users:Users`,
    data: {
      permissions: {
        only: [PermissionId.UsersRead]
      }
    },
    loadComponent: () => import('./features/users/user-list/user-list.component').then(module => module.UserListComponent)
  },
  {
    path: RouteSegments.products,
    canActivate: [authGuard],
    canActivateChild: [entryPermissionGuard],
    title: $localize`:@@route.products:Products`,
    data: {
      permissions: {
        only: [PermissionId.ProductsRead]
      }
    },
    loadComponent: () => import('./features/products/product-list/product-list.component').then(module => module.ProductListComponent)
  },
  ...userRoutes,
  ...productRoutes
];
