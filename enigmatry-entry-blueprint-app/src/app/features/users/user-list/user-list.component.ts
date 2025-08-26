import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GetUsersResponseItem, PermissionId, UsersClient } from '@api';
import { PermissionService } from '@app/auth/permissions.service';
import { ContextMenuItem, PagedData } from '@enigmatry/entry-components/table';
import { BaseListComponent } from '@shared/list-component/base-list-component.model';
import { SearchFilterPagedQuery } from '@shared/list-component/search-filter-paged-query';
import { RouteSegments } from '@shared/model/route-segments';
import { Observable, Subscription, switchMap, tap } from 'rxjs';
import { GetUsersQuery } from '../models/qet-users-query.model';

@Component({
  standalone: false,
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent extends BaseListComponent implements OnInit {
  contextMenuItems: ContextMenuItem[] = [];
  data: PagedData<GetUsersResponseItem>;
  override query = new GetUsersQuery();

  constructor(private client: UsersClient, private permissionService: PermissionService,
    protected override router: Router, protected override activatedRoute: ActivatedRoute
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

  readonly onRowSelected = async(rowData: GetUsersResponseItem) => {
    await this.router.navigate([RouteSegments.edit, rowData.id], { relativeTo: this.activatedRoute });
  };

  readonly onContextMenuItemSelected = async(contextMenuItem: { itemId: string; rowData: GetUsersResponseItem }) => {
    if (contextMenuItem.itemId === RouteSegments.edit) {
      await this.router.navigate([RouteSegments.edit, contextMenuItem.rowData.id], { relativeTo: this.activatedRoute });
    }
  };

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
