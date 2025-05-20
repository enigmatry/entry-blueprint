import { ActivatedRoute, Params, Router } from '@angular/router';
import { SearchFilterParams } from '@enigmatry/entry-components/search-filter';
import { PageEvent, SortEvent } from '@enigmatry/entry-components/table';
import { SearchFilterPagedQuery } from './search-filter-paged-query';

export abstract class BaseListComponent {
  query: SearchFilterPagedQuery;

  constructor(protected router: Router, protected activatedRoute: ActivatedRoute) { }

  // Handle search filters change
  readonly searchFilterChange = async(searchParams: SearchFilterParams) => {
    this.query.searchFilterChange(searchParams);
    await this.updateRouteQueryString(this.query.getRouteQueryParams());
  };

  // Handle page change
  readonly pageChange = async(page: PageEvent) => {
    this.query.pageChange(page);
    await this.updateRouteQueryString(this.query.getRouteQueryParams());
  };

  // Handle sort change
  readonly sortChange = async(sort: SortEvent) => {
    this.query.sortChange(sort);
    await this.updateRouteQueryString(this.query.getRouteQueryParams());
  };

  // Appends new query params to the current route
  private readonly updateRouteQueryString = async(queryParams: Params) => {
    await this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams,
      queryParamsHandling: 'merge'
    });
  };
}
