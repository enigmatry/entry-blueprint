import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductsGeneratedModule } from './generated/products-generated.module';
import { ProductsRoutingModule } from './products-routing.module';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';
import { GridCellsModule } from 'src/app/shared/grid-cells/grid-cells.module';

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
