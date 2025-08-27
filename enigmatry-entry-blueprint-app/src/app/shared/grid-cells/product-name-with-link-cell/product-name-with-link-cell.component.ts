import { Component, input } from '@angular/core';
import { GetProductsResponseItem } from '@api';

@Component({
    standalone: false,
    selector: 'app-product-name-with-link-cell',
    templateUrl: './product-name-with-link-cell.component.html',
    styleUrls: ['./product-name-with-link-cell.component.scss']
})
export class ProductNameWithLinkCellComponent {
    readonly rowData = input.required<GetProductsResponseItem>();
    readonly colDef = input.required<unknown>();
}
