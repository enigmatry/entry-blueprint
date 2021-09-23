import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductNameWithLinkCellComponent } from './product-name-with-link-cell/product-name-with-link-cell.component';
import { ProductTypeCellComponent } from './product-type-cell/product-type-cell.component';
import { MaterialModule } from '../material.module';

@NgModule({
    declarations: [
        ProductNameWithLinkCellComponent,
        ProductTypeCellComponent
    ],
    imports: [
        CommonModule,
        MaterialModule
    ],
    exports: [
        ProductNameWithLinkCellComponent,
        ProductTypeCellComponent
    ]
})
export class GridCellsModule { }
