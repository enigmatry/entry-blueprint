import { Component, Input } from '@angular/core';
import { GetProductsResponseItem } from '@api';

@Component({
    standalone: false,
    selector: 'app-product-name-with-link-cell',
    templateUrl: './product-name-with-link-cell.component.html',
    styleUrls: ['./product-name-with-link-cell.component.scss']
})
export class ProductNameWithLinkCellComponent {
    @Input() rowData: GetProductsResponseItem;
    @Input() colDef: any;
}
