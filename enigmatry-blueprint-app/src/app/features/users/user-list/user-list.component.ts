import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContextMenuItem, PagedData } from '@enigmatry/entry-components/table';
import { Observable, Subscription, switchMap, tap } from 'rxjs';
import { GetUsersResponseItem, PermissionId, UsersClient } from 'src/app/api/api-reference';
import { PermissionService } from 'src/app/core/auth/permissions.service';
import { BaseListComponent } from 'src/app/shared/list-component/base-list-component.model';
import { SearchFilterPagedQuery } from 'src/app/shared/list-component/search-filter-paged-query';
import { RouteSegments } from 'src/app/shared/model/route-segments';
import { GetUsersQuery } from '../models/qet-users-query.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent extends BaseListComponent implements OnInit {
  contextMenuItems: ContextMenuItem[] = [];
  data: PagedData<GetUsersResponseItem>;
  query = new GetUsersQuery();

  constructor(private client: UsersClient, private permissionService: PermissionService,
    protected router: Router, protected activatedRoute: ActivatedRoute
  ) {
    super(router, activatedRoute);
  }

  ngOnInit(): void {
    this.watchQueryParamsAndGetUsers();
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

  private getUsers(query: SearchFilterPagedQuery): Observable<PagedData<GetUsersResponseItem>> {
    return this.client.search(this.query.name.value, this.query.email.value,
      query.pageNumber, query.pageSize, query.sortBy, query.sortDirection);
  }

  // Subscribe to activated route and load list when query params change
  private watchQueryParamsAndGetUsers(): Subscription {
    return this.activatedRoute.queryParams
      .pipe(
        tap(params => this.query.applyRouteChanges(params)),
        switchMap(() => this.getUsers(this.query))
      )
      .subscribe(response => this.data = response);
  }
}
