import { NgModule } from '@angular/core';
import { ProductNameWithLinkCellComponent } from './grid-cells/product-name-with-link-cell/product-name-with-link-cell.component';
import { ProductTypeCellComponent } from './grid-cells/product-type-cell/product-type-cell.component';

@NgModule({
    imports: [
        ProductNameWithLinkCellComponent,
        ProductTypeCellComponent
    ],
    exports: [
        ProductNameWithLinkCellComponent,
        ProductTypeCellComponent
    ]
})
export class StandaloneModule { }