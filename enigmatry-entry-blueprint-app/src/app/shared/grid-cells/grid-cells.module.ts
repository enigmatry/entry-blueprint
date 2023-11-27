import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '../material.module';
import { ProductNameWithLinkCellComponent } from './product-name-with-link-cell/product-name-with-link-cell.component';
import { ProductTypeCellComponent } from './product-type-cell/product-type-cell.component';

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
