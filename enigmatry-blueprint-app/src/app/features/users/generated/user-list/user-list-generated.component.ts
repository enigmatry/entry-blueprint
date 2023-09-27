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
import { PagedData, SortDirection, CellTemplate, ContextMenuItem, RowContextMenuFormatter, RowClassFormatter, RowSelectionFormatter, ColumnDef, PageEvent, SortEvent } from '@enigmatry/entry-table';

import { GetUsersResponseItem } from 'src/app/api/api-reference';

@Component({
  selector: 'app-g-user-list',
  templateUrl: './user-list-generated.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserListGeneratedComponent implements OnInit {

  @Input() data: PagedData<GetUsersResponseItem> | null;
  @Input() loading: boolean;

  @Input() showPaginator = true;
  @Input() showFirstLastButtons = false;
  @Input() pageSizeOptions = [20, 50, 100];
  @Input() hidePageSize = !true;

  @Input() defaultSort: { sortBy?: string | undefined, sortDirection?: SortDirection } = { };

  @Input() rowSelectable = true;
  @Input() multiSelectable = true;
  @Input() showSelectAllCheckbox = false;
  @Input() rowSelected: GetUsersResponseItem[] = [];

  @Input() headerTemplate: TemplateRef<any> | CellTemplate;
  @Input() cellTemplate: TemplateRef<any> | CellTemplate;
      
  @Input() noResultText: string;
  @Input() noResultTemplate: TemplateRef<any> | null;

  @Input() showContextMenu = true;
  @Input() contextMenuItems: ContextMenuItem[] = [];
  @Input() rowContextMenuFormatter: RowContextMenuFormatter;
  
  @Input() rowClassFormatter: RowClassFormatter;
  @Input() rowSelectionFormatter: RowSelectionFormatter = {};

  @Input() columns: ColumnDef[] = [];

  @Output() pageChange = new EventEmitter<PageEvent>();
  @Output() sortChange = new EventEmitter<SortEvent>();
  @Output() selectionChange = new EventEmitter<GetUsersResponseItem[]>();
  @Output() contextMenuItemSelected = new EventEmitter<{ itemId: string; rowData: GetUsersResponseItem }>();
  @Output() rowClick = new EventEmitter<GetUsersResponseItem>();



  constructor() { }

  ngOnInit(): void {
   this.columns = [
{ field: 'id', hide: true, sortable: true },
{ field: 'emailAddress', header: $localize `:@@users.user-list.email-address:E-mail`, hide: false, sortable: true },
{ field: 'fullName', header: $localize `:@@users.user-list.full-name:Full name`, hide: false, sortable: true },
{ field: 'createdOn', header: $localize `:@@users.user-list.created-on:Created on`, hide: false, sortable: true, type: 'date', typeParameter: { name: 'date' } },
{ field: 'updatedOn', header: $localize `:@@users.user-list.updated-on:Updated on`, hide: false, sortable: true, type: 'date', typeParameter: { name: 'date' } }
];
  }
}
