import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { ProductsClient } from '@api';
import { RouteSegments } from '@shared/model/route-segments';

export const productRoutes: Routes = [
	{
		path: `${RouteSegments.products}/${RouteSegments.create}`,
		loadComponent: () => import('./product-edit/product-edit.component').then(module => module.ProductEditComponent),
		title: $localize`:@@route.products.create:Create product`
	},
	{
		path: `${RouteSegments.products}/${RouteSegments.edit}/:id`,
		loadComponent: () => import('./product-edit/product-edit.component').then(module => module.ProductEditComponent),
		resolve: {
			response: (route: ActivatedRouteSnapshot) => inject(ProductsClient).get(route.params['id'])
		},
		title: $localize`:@@route.products.edit:Update product`
	}
];
