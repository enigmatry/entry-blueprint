import { Component, input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { GetProductsResponseItem, ProductType } from '@api';

@Component({
    imports: [MatIconModule],
    selector: 'app-product-type-cell',
    templateUrl: './product-type-cell.component.html',
    styleUrls: ['./product-type-cell.component.scss']
})
export class ProductTypeCellComponent {
    readonly rowData = input.required<GetProductsResponseItem>();
    readonly colDef = input.required<unknown>();

    productTypes = ProductType;
}
