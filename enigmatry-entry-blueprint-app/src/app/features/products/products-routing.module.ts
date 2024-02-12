import { NgModule, inject } from '@angular/core';
import { ActivatedRouteSnapshot, RouterModule, Routes } from '@angular/router';
import { ProductsClient } from 'src/app/api/api-reference';
import { RouteSegments } from 'src/app/shared/model/route-segments';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';

const routes: Routes = [
	{
		path: '',
		component: ProductListComponent,
		title: $localize`:@@route.products:Products`
	},
	{
		path: RouteSegments.create,
		component: ProductEditComponent,
		title: $localize`:@@route.products.create:Create product`
	},
	{
		path: `${RouteSegments.edit}/:id`,
		component: ProductEditComponent,
		resolve: {
			response: (route: ActivatedRouteSnapshot) => inject(ProductsClient).get(route.params.id)
		},
		title: $localize`:@@route.products.edit:Update product`
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class ProductsRoutingModule { }
