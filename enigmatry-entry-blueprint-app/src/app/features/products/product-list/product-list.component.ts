import { Component, inject, OnInit } from '@angular/core';
import { GetProductsResponseItem, PermissionId, ProductsClient } from '@api';
import { PermissionService } from '@app/auth/permissions.service';
import { ContextMenuItem, PagedData } from '@enigmatry/entry-components/table';
import { BaseListComponent } from '@shared/list-component/base-list-component.model';
import { RouteSegments } from '@shared/model/route-segments';
import { Observable, Subscription, switchMap, tap } from 'rxjs';
import { GetProductsQuery } from '../models/get-products-query.model';

@Component({
  standalone: false,
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent extends BaseListComponent implements OnInit {
  data: PagedData<GetProductsResponseItem>;
  override query = new GetProductsQuery();
  PermissionId = PermissionId;
  contextMenuItems: ContextMenuItem[] = [];
  private readonly permissionService: PermissionService = inject(PermissionService);
  private readonly client: ProductsClient = inject(ProductsClient);

  ngOnInit(): void {
    this.watchQueryParamsAndGetProducts();
    this.initContextMenuItems();
  }

  readonly onRowSelected = async(rowData: GetProductsResponseItem) => {
    await this.router.navigate([RouteSegments.edit, rowData.id], { relativeTo: this.activatedRoute });
  };

  readonly onContextMenuItemSelected = async(contextMenuItem: { itemId: string; rowData: GetProductsResponseItem }) => {
    if (contextMenuItem.itemId === RouteSegments.edit) {
      await this.router.navigate([RouteSegments.edit, contextMenuItem.rowData.id], { relativeTo: this.activatedRoute });
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
    this.client.search(query.name.value, query.code.value, query.expiresBeforeDate(),
      query.pageNumber, query.pageSize, query.sortBy, query.sortDirection);
}
