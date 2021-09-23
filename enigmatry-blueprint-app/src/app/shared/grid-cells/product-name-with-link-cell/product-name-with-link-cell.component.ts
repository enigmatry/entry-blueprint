import { Component, Input } from '@angular/core';
import { GetProductsResponseItem } from 'src/app/api/api-reference';

@Component({
    selector: 'app-product-name-with-link-cell',
    templateUrl: './product-name-with-link-cell.component.html',
    styleUrls: ['./product-name-with-link-cell.component.scss']
})
// eslint-disable-next-line id-length
export class ProductNameWithLinkCellComponent {
    @Input() rowData: GetProductsResponseItem;
    @Input() colDef: any;
}
