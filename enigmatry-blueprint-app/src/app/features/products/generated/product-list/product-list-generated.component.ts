// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------;
/* eslint-disable */
import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { CellTemplate, ColumnDef, ContextMenuItem, RowContextMenuFormatter } from '@enigmatry/angular-building-blocks/enigmatry-grid';
import { PagedData, PageEvent, SortDirection, SortEvent } from '@enigmatry/angular-building-blocks/pagination';

import { GetProductsResponseItem } from 'src/app/api/api-reference';

@Component({
  selector: 'app-g-product-list',
  templateUrl: './product-list-generated.component.html',
  styleUrls: ['./product-list-generated.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductListGeneratedComponent implements OnInit {

  @Input() data: PagedData<GetProductsResponseItem> | null;
  @Input() loading: boolean;

  @Input() showPaginator = true;
  @Input() showFirstLastButtons = true;
  @Input() pageSizeOptions = [2, 5, 10, 25, 50];
  @Input() hidePageSize = !true;

  @Input() defaultSort: { sortBy?: string | undefined, sortDirection?: SortDirection } = { };

  @Input() rowSelectable = false;
  @Input() multiSelectable = false;

  @Input() headerTemplate: TemplateRef<any> | CellTemplate;
  @Input() cellTemplate: TemplateRef<any> | CellTemplate;

  @Input() showContextMenu = true;
  @Input() contextMenuItems: ContextMenuItem[] = [];
  @Input() rowContextMenuFormatter: RowContextMenuFormatter;

  @Input() columns: ColumnDef[] = [];

  @Output() pageChange = new EventEmitter<PageEvent>();
  @Output() sortChange = new EventEmitter<SortEvent>();
  @Output() selectionChange = new EventEmitter<GetProductsResponseItem[]>();
  @Output() contextMenuItemSelected = new EventEmitter<{ itemId: string; rowData: GetProductsResponseItem }>();
  @Output() rowClick = new EventEmitter<GetProductsResponseItem>();

@ViewChild('nameTpl', { static: true }) nameTpl: TemplateRef<any>;
@ViewChild('typeTpl', { static: true }) typeTpl: TemplateRef<any>;

  constructor() { }

  ngOnInit(): void {
    this.columns = [
{ field: 'id', hide: true, sortable: true },
{ field: 'name', header: $localize `:@@products.product-list.name:Product name`, hide: false, sortable: true, cellTemplate: this.nameTpl },
{ field: 'code', header: $localize `:@@products.product-list.code:Code`, hide: false, sortable: true },
{ field: 'type', header: $localize `:@@products.product-list.type:Type`, hide: false, sortable: true, cellTemplate: this.typeTpl },
{ field: 'price', header: $localize `:@@products.price:Price per unit`, hide: false, sortable: true, type: 'currency', typeParameter: { currencyCode: 'EUR', display: '€', digitsInfo: '', locale: '' }, class: 'products-price' },
{ field: 'amount', header: $localize `:@@products.amount:Units`, hide: false, sortable: true },
{ field: 'contactEmail', header: $localize `:@@products.product-list.contact-email:Contact email`, hide: false, sortable: true },
{ field: 'contactPhone', header: $localize `:@@products.product-list.contact-phone:Contact phone`, hide: false, sortable: true },
{ field: 'infoLink', hide: true, sortable: true },
{ field: 'expiresOn', header: $localize `:@@products.product-list.expires-on:Expires on`, hide: false, sortable: true, type: 'date', typeParameter: undefined },
{ field: 'freeShipping', header: $localize `:@@products.product-list.free-shipping:Free shipping`, hide: false, sortable: true, type: 'boolean', typeParameter: undefined }
];
    this.contextMenuItems = [];
  }
}
