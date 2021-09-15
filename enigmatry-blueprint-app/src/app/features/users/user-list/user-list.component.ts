import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { PagedData } from '@enigmatry/angular-building-blocks/pagination';
import { GetUsersResponseItem, UsersClient } from 'src/app/api/api-reference';
import { ListComponentWithRouting } from 'src/app/shared/list-component/list-component-with-routing.model';
import { GetUsersQuery } from '../models/get-users-query.model';

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.scss']
})
export class UserListComponent extends ListComponentWithRouting<GetUsersResponseItem, GetUsersQuery> implements OnInit {
    constructor(private client: UsersClient, protected router: Router, protected activatedRoute: ActivatedRoute) {
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
    }

    onItemSelected(contextMenuItem: { itemId: string; rowData: GetUsersResponseItem }) {
        if (contextMenuItem.itemId === 'edit') {
            this.router.navigate(['edit', contextMenuItem.rowData.id], { relativeTo: this.activatedRoute });
        }
    }
}
