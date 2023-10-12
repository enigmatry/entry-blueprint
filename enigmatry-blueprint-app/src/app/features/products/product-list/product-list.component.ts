import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContextMenuItem, PagedData } from '@enigmatry/entry-table';
import { Observable, Subscription, switchMap, tap } from 'rxjs';
import { GetProductsResponseItem, PermissionId, ProductsClient } from 'src/app/api/api-reference';
import { PermissionService } from 'src/app/core/auth/permissions.service';
import { BaseListComponent } from 'src/app/shared/list-component/base-list-component.model';
import { RouteSegments } from 'src/app/shared/model/route-segments';
import { GetProductsQuery } from '../models/get-products-query.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent extends BaseListComponent implements OnInit {
  data: PagedData<GetProductsResponseItem>;
  query = new GetProductsQuery();
  PermissionId = PermissionId;
  contextMenuItems: ContextMenuItem[] = [];

  constructor(private client: ProductsClient,
    protected router: Router,
    protected activatedRoute: ActivatedRoute,
    private permissionService: PermissionService) {
    super(router, activatedRoute);
  }

  ngOnInit(): void {
    this.watchQueryParamsAndGetProducts();
    this.initContextMenuItems();
  }


  onRowSelected(rowData: GetProductsResponseItem) {
    this.router.navigate([RouteSegments.edit, rowData.id], { relativeTo: this.activatedRoute });
  }

  onContextMenuItemSelected = (contextMenuItem: { itemId: string; rowData: GetProductsResponseItem }) => {
    if (contextMenuItem.itemId === RouteSegments.edit) {
      this.router.navigate([RouteSegments.edit, contextMenuItem.rowData.id], { relativeTo: this.activatedRoute });
    } else if (contextMenuItem.itemId === 'delete' && contextMenuItem.rowData.id !== undefined) {
      this.client
        .remove(contextMenuItem.rowData.id)
        .subscribe(() => this.getProducts(this.query));
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

  private watchQueryParamsAndGetProducts(): Subscription {
    return this.activatedRoute.queryParams
      .pipe(
        tap(params => this.query.applyRouteChanges(params)),
        switchMap(() => this.getProducts(this.query))
      )
      .subscribe(response => this.data = response);
  }

  private getProducts = (query: GetProductsQuery): Observable<PagedData<GetProductsResponseItem>> =>
    this.client.search(query.name.value, query.code.value, query.contactEmail.value,
      query.pageNumber, query.pageSize, query.sortBy, query.sortDirection);
}
