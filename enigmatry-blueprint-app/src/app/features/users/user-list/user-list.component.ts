import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ContextMenuItem, PagedData } from '@enigmatry/entry-table';
import { Observable } from 'rxjs';
import { GetUsersResponseItem, PermissionId, UsersClient } from 'src/app/api/api-reference';
import { PermissionService } from 'src/app/core/auth/permissions.service';
import { ListComponentWithRouting } from 'src/app/shared/list-component/list-component-with-routing.model';
import { RouteSegments } from 'src/app/shared/model/route-segments';
import { GetUsersQuery } from '../models/get-users-query.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent extends ListComponentWithRouting<GetUsersResponseItem, GetUsersQuery> implements OnInit {
  contextMenuItems: ContextMenuItem[] = [];

  constructor(private client: UsersClient, private permissionService: PermissionService,
    protected router: Router, protected activatedRoute: ActivatedRoute
  ) {
    super();
    this.query = new GetUsersQuery();
  }

  fetchData(query: GetUsersQuery): Observable<PagedData<GetUsersResponseItem>> {
    return this.client.search(query.keyword, query.pageNumber, query.pageSize, query.sortBy, query.sortDirection);
  }

  createQueryInstance(routeParams: Params, queryParams: Params): GetUsersQuery {
    const result = new GetUsersQuery();
    result.applyRouteChanges(routeParams, queryParams);
    return result;
  }

  ngOnInit(): void {
    this.watchRouteParams(this.router, this.activatedRoute);
    this.initContextMenuItems();
  }

  initContextMenuItems(): void {
    this.contextMenuItems = [{
      id: RouteSegments.edit,
      name: $localize`:@@users.user-list.context.edit:Edit`,
      disabled: !this.permissionService.hasPermissions([PermissionId.UsersWrite]),
      icon: 'edit'
    }];
  }

  onRowSelected(rowData: GetUsersResponseItem) {
    this.router.navigate([RouteSegments.edit, rowData.id], { relativeTo: this.activatedRoute });
  }

  onContextMenuItemSelected(contextMenuItem: { itemId: string; rowData: GetUsersResponseItem }) {
    if (contextMenuItem.itemId === RouteSegments.edit) {
      this.router.navigate([RouteSegments.edit, contextMenuItem.rowData.id], { relativeTo: this.activatedRoute });
    }
  }
}
