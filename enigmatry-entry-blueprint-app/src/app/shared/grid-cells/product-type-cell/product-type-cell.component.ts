import { Component, Input } from '@angular/core';
import { GetProductsResponseItem, ProductType } from '@api';

@Component({
    standalone: false,
    selector: 'app-product-type-cell',
    templateUrl: './product-type-cell.component.html',
    styleUrls: ['./product-type-cell.component.scss']
})
export class ProductTypeCellComponent {
    @Input() rowData: GetProductsResponseItem;
    @Input() colDef: any;

    productTypes = ProductType;
}
