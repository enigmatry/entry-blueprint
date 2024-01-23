import { NgModule, inject } from '@angular/core';
import { ActivatedRouteSnapshot, RouterModule, Routes } from '@angular/router';
import { ProductsClient } from 'src/app/api/api-reference';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';

const routes: Routes = [
	{
		path: '',
		component: ProductListComponent,
		title: 'Products'
	},
	{
		path: `create`,
		component: ProductEditComponent,
		title: 'New product'
	},
	{
		path: 'edit/:id',
		component: ProductEditComponent,
		resolve: {
			response: (route: ActivatedRouteSnapshot) => inject(ProductsClient).get(route.params.id)
		},
		title: 'Edit product'
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class ProductsRoutingModule { }
