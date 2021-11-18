import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductResolverService } from './services/product-resolver.service';

const routes: Routes = [
	{
		path: '',
		component: ProductListComponent
	},
	{
		path: `create`,
		component: ProductEditComponent
	},
	{
		path: 'edit/:id',
		component: ProductEditComponent,
		resolve: { response: ProductResolverService }
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class ProductsRoutingModule { }
