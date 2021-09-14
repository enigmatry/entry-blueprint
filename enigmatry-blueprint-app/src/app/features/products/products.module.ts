import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductsGeneratedModule } from './generated/products-generated.module';
import { ProductsRoutingModule } from './products-routing.module';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';

@NgModule({
    declarations: [
        ProductEditComponent,
        ProductListComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        ProductsGeneratedModule,
        ProductsRoutingModule
    ]
})
export class ProductsModule { }
