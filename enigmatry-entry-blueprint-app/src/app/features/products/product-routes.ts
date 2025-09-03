import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { PermissionId, ProductsClient } from '@api';
import { authGuard } from '@app/auth/auth.guard';
import { RouteSegments } from '@shared/model/route-segments';
import { ProductEditComponent } from './product-edit/product-edit.component';

export const productRoutes: Routes = [
	{
		path: `${RouteSegments.products}/${RouteSegments.create}`,
		component: ProductEditComponent,
		canActivate: [authGuard],
		data: {
			permissions: {
				only: [PermissionId.ProductsWrite]
			}
		},
		title: $localize`:@@route.products.create:Create product`
	},
	{
		path: `${RouteSegments.products}/${RouteSegments.edit}/:id`,
		component: ProductEditComponent,
		canActivate: [authGuard],
		data: {
			permissions: {
				only: [PermissionId.ProductsWrite]
			}
		},
		resolve: {
			response: (route: ActivatedRouteSnapshot) => inject(ProductsClient).get(route.params['id'])
		},
		title: $localize`:@@route.products.edit:Update product`
	}
];
