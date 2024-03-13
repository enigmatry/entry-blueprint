import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PermissionId } from '@api';
import { authGuard } from '@app/auth/auth.guard';
import { entryPermissionGuard } from '@enigmatry/entry-components/permissions';
import { HomeComponent } from '@features/home/home.component';


const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [authGuard],
        title: $localize`:@@route.home:Home`
    },
    {
        path: 'users',
        canActivate: [authGuard],
        canActivateChild: [entryPermissionGuard],
        data: {
          permissions: {
            only: [PermissionId.UsersRead]
          }
        },
        loadChildren: () => import('./features/users/users.module').then(module => module.UsersModule)
    },
    {
        path: 'products',
        canActivate: [authGuard],
        canActivateChild: [entryPermissionGuard],
        data: {
          permissions: {
            only: [PermissionId.ProductsRead]
          }
        },
        loadChildren: () => import('./features/products/products.module').then(module => module.ProductsModule)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
