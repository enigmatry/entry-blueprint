/* eslint-disable id-length */
import { ContextMenuItem, RowContextMenuFormatter } from '@enigmatry/angular-building-blocks';
import { GetProductsResponseItem, ProductType } from 'src/app/api/api-reference';

export class ProductListRowContextMenuFormatter implements RowContextMenuFormatter {
    items = (rowData: GetProductsResponseItem): ContextMenuItem[] => [
        {
            id: 'edit',
            name: $localize`:@@products.product-list.context.edit:Edit`,
            icon: 'edit',
            disabled: false
        },
        {
            id: 'delete',
            name: $localize`:@@products.product-list.context.delete:Delete`,
            icon: 'delete',
            disabled: rowData.type === ProductType.Book
        }
    ];
}
