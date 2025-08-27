import { Component, input } from '@angular/core';
import { GetProductsResponseItem, ProductType } from '@api';

@Component({
    standalone: false,
    selector: 'app-product-type-cell',
    templateUrl: './product-type-cell.component.html',
    styleUrls: ['./product-type-cell.component.scss']
})
export class ProductTypeCellComponent {
    readonly rowData = input.required<GetProductsResponseItem>();
    readonly colDef = input.required<unknown>();

    productTypes = ProductType;
}
