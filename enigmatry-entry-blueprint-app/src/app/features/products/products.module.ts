import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GridCellsModule } from '@shared/grid-cells/grid-cells.module';
import { SharedModule } from '@shared/shared.module';
import { ProductsGeneratedModule } from './generated/products-generated.module';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductsRoutingModule } from './products-routing.module';

@NgModule({
    declarations: [
        ProductEditComponent,
        ProductListComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        GridCellsModule,
        ProductsGeneratedModule,
        ProductsRoutingModule
    ]
})
export class ProductsModule { }
