import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/ckeditor-demo',
        pathMatch: 'full'
    },
    {
        path: 'users',
        loadChildren: () => import('./features/users/users.module').then(module => module.UsersModule)
    },
    {
        path: 'products',
        loadChildren: () => import('./features/products/products.module').then(module => module.ProductsModule)
    },
    {
        path: 'ckeditor-demo',
        loadChildren: () => import('./features/ckeditor-demo/ckeditor-demo.module')
            .then(module => module.CkeditorDemoModule)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
