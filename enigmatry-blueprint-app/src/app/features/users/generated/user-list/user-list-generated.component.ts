// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------;
import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { CellTemplate, ColumnDef, ContextMenuItem } from '@enigmatry/angular-building-blocks/enigmatry-grid';
import { PagedData, PageEvent, SortEvent } from '@enigmatry/angular-building-blocks/pagination';

import { GetUsersResponseItem } from 'src/app/api/api-reference';

@Component({
  selector: 'app-g-user-list',
  templateUrl: './user-list-generated.component.html',
  styleUrls: ['./user-list-generated.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserListGeneratedComponent implements OnInit {

  @Input() data: PagedData<GetUsersResponseItem> | null;
  @Input() loading: boolean;

  @Input() showPaginator = true;
  @Input() showFirstLastButtons = false;
  @Input() pageSizeOptions = [10, 50, 100];
  @Input() hidePageSize = !true;

  @Input() rowSelectable = true;
  @Input() multiSelectable = false;

  @Input() headerTemplate: TemplateRef<any> | CellTemplate;
  @Input() cellTemplate: TemplateRef<any> | CellTemplate;

  @Input() showContextMenu = true;
  @Input() contextMenuItems: ContextMenuItem[] = [];

  @Input() columns: ColumnDef[] = [];

  @Output() pageChange = new EventEmitter<PageEvent>();
  @Output() sortChange = new EventEmitter<SortEvent>();
  @Output() selectionChange = new EventEmitter<GetUsersResponseItem[]>();
  @Output() contextMenuItemSelected = new EventEmitter<{ itemId: string; rowData: GetUsersResponseItem }>();

  

  constructor() {
    this.columns = [
{ field: 'id', header: 'Id', hide: true, sortable: true },
{ field: 'userName', header: 'E-mail', hide: false, sortable: true },
{ field: 'name', header: 'Name', hide: false, sortable: true },
{ field: 'createdOn', header: 'Created on', hide: false, sortable: true, type: 'date', typeParameter: undefined },
{ field: 'updatedOn', header: 'Updated on', hide: false, sortable: true, type: 'date', typeParameter: undefined }
];
    this.contextMenuItems = [
{ id: 'edit', name: 'Edit', icon: 'edit' }
];
  }

  ngOnInit(): void {
  }
}
