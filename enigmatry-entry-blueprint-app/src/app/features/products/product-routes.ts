import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { ProductsClient } from '@api';
import { RouteSegments } from '@shared/model/route-segments';
import { ProductEditComponent } from './product-edit/product-edit.component';

export const productRoutes: Routes = [
	{
		path: `${RouteSegments.products}/${RouteSegments.create}`,
		component: ProductEditComponent,
		title: $localize`:@@route.products.create:Create product`
	},
	{
		path: `${RouteSegments.products}/${RouteSegments.edit}/:id`,
		component: ProductEditComponent,
		resolve: {
			response: (route: ActivatedRouteSnapshot) => inject(ProductsClient).get(route.params['id'])
		},
		title: $localize`:@@route.products.edit:Update product`
	}
];
