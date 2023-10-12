import { ActivatedRoute, Params, Router } from '@angular/router';
import { SearchFilterParams } from '@enigmatry/entry-components/search-filter';
import { PageEvent, SortEvent } from '@enigmatry/entry-table';
import { SearchFilterPagedQuery } from './search-filter-paged-query';

export abstract class BaseListComponent {
  query: SearchFilterPagedQuery;

  constructor(protected router: Router, protected activatedRoute: ActivatedRoute) { }

  // Handle search filters change
  searchFilterChange(searchParams: SearchFilterParams) {
    this.query.searchFilterChange(searchParams);
    this.updateRouteQueryString(this.query.getRouteQueryParams());
  }

  // Handle page change
  pageChange(page: PageEvent): void {
    this.query.pageChange(page);
    this.updateRouteQueryString(this.query.getRouteQueryParams());
  }

  // Handle sort change
  sortChange(sort: SortEvent): void {
    this.query.sortChange(sort);
    this.updateRouteQueryString(this.query.getRouteQueryParams());
  }

  // Appends new query params to the current route
  private updateRouteQueryString(queryParams: Params): void {
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams,
      queryParamsHandling: 'merge'
    });
  }
}
