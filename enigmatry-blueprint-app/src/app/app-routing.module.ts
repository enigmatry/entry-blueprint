import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { authGuard } from './core/auth/auth.guard';
import { HomeComponent } from './features/home/home.component';

const routes: Routes = [
    { path: '', redirectTo: '/users', pathMatch: 'full' },
    {
        path: '',
        canActivate: [authGuard],
        canActivateChild: [authGuard],
        component: HomeComponent,
        children: [
            {
                path: 'users',
                loadChildren: () => import('./features/users/users.module').then(module => module.UsersModule)
            },
            {
                path: 'products',
                loadChildren: () => import('./features/products/products.module').then(module => module.ProductsModule)
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
