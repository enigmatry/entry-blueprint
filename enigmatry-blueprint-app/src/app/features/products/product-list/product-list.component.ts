import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PagedData } from '@enigmatry/angular-building-blocks';
import { Observable } from 'rxjs';
import { GetProductsResponseItem, ProductsClient } from 'src/app/api/api-reference';
import { ListComponentWithRouting } from 'src/app/shared/list-component/list-component-with-routing.model';
import { GetProductsQuery } from '../models/get-products-query.model';

@Component({
    selector: 'app-product-list',
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent
    extends ListComponentWithRouting<GetProductsResponseItem, GetProductsQuery>
    implements OnInit {
    constructor(private client: ProductsClient, protected router: Router, protected activatedRoute: ActivatedRoute) {
        super();
        this.query = new GetProductsQuery();
    }

    fetchData(query: GetProductsQuery): Observable<PagedData<GetProductsResponseItem>> {
        return this.client.search(query.keyword, query.pageNumber, query.pageSize, query.sortBy, query.sortDirection);
    }

    ngOnInit(): void {
        this.watchRouteParams(this.router, this.activatedRoute);
    }

    onItemSelected(contextMenuItem: { itemId: string; rowData: GetProductsResponseItem }) {
        if (contextMenuItem.itemId === 'edit') {
            this.router.navigate(['edit', contextMenuItem.rowData.id], { relativeTo: this.activatedRoute });
        }
    }
}
