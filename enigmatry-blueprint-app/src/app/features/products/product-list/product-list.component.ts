import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ContextMenuItem, PagedData } from '@enigmatry/entry-table';
import { Observable } from 'rxjs';
import { GetProductsResponseItem, PermissionId, ProductsClient } from 'src/app/api/api-reference';
import { PermissionService } from 'src/app/core/auth/permissions.service';
import { ListComponentWithRouting } from 'src/app/shared/list-component/list-component-with-routing.model';
import { RouteSegments } from 'src/app/shared/model/route-segments';
import { GetProductsQuery } from '../models/get-products-query.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent
  extends ListComponentWithRouting<GetProductsResponseItem, GetProductsQuery> implements OnInit {
  PermissionId = PermissionId;
  contextMenuItems: ContextMenuItem[] = [];

  constructor(private client: ProductsClient,
    protected router: Router,
    protected activatedRoute: ActivatedRoute,
    private permissionService: PermissionService) {
    super();
  }

  ngOnInit(): void {
    this.watchRouteParams(this.router, this.activatedRoute);
    this.initContextMenuItems();
  }

  fetchData = (query: GetProductsQuery): Observable<PagedData<GetProductsResponseItem>> =>
    this.client.search(query.keyword, query.pageNumber, query.pageSize, query.sortBy, query.sortDirection);

  createQueryInstance = (routeParams: Params, queryParams: Params): GetProductsQuery => {
    const query = new GetProductsQuery();
    query.applyRouteChanges(routeParams, queryParams);
    return query;
  };

  onRowSelected(rowData: GetProductsResponseItem) {
    this.router.navigate([RouteSegments.edit, rowData.id], { relativeTo: this.activatedRoute });
  }

  onContextMenuItemSelected = (contextMenuItem: { itemId: string; rowData: GetProductsResponseItem }) => {
    if (contextMenuItem.itemId === RouteSegments.edit) {
      this.router.navigate([RouteSegments.edit, contextMenuItem.rowData.id], { relativeTo: this.activatedRoute });
    } else if (contextMenuItem.itemId === 'delete' && contextMenuItem.rowData.id !== undefined) {
      this.client
        .remove(contextMenuItem.rowData.id)
        .subscribe(() => this.loadList());
    }
  };

  private initContextMenuItems(): void {
    this.contextMenuItems = [{
      id: 'edit',
      name: $localize`:@@products.product-list.context.edit:Edit`,
      icon: 'edit',
      disabled: !this.permissionService.hasPermissions([PermissionId.ProductsWrite])
    },
    {
      id: 'delete',
      name: $localize`:@@products.product-list.context.delete:Delete`,
      icon: 'delete',
      disabled: !this.permissionService.hasPermissions([PermissionId.ProductsDelete])
    }];
  }
}
