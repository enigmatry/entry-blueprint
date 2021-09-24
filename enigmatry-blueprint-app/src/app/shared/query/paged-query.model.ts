import { Params } from '@angular/router';
import { OnPage, OnSort, PageEvent, SortDirection, SortEvent } from '@enigmatry/angular-building-blocks/pagination';
import { RouteAwareQuery } from './query.interface';

export const defaultPageSize = 10;
export const defaultPageNumber = 1;

export class PagedQuery implements OnPage, OnSort, RouteAwareQuery {
  keyword?: string;
  pageNumber = defaultPageNumber;
  pageSize = defaultPageSize;
  sortBy?: string;
  sortDirection?: SortDirection;

  sortChange(sort: SortEvent): void {
    if (sort.active) {
      this.sortBy = sort.active;
      this.sortDirection = sort.direction;
      this.pageNumber = defaultPageNumber;
    }
  }

  pageChange(page: PageEvent): void {
    this.pageNumber = page.pageIndex + 1;
    this.pageSize = page.pageSize;
  }

  applyRouteChanges(routeParams: Params, queryParams: Params): void {
    this.keyword = queryParams.keyword;
    this.pageNumber = queryParams.pageNumber ? Number(queryParams.pageNumber) : defaultPageNumber;
    this.pageSize = queryParams.pageSize ? Number(queryParams.pageSize) : defaultPageSize;
    this.sortBy = queryParams.sortBy ?? this.sortBy;
    this.sortDirection = queryParams.sortDirection ?? this.sortDirection;
  }

  getRouteQueryParams(): Params {
    // All properties are added to query params
    return { ...this };
  }

  getPagedApiRequestParams(): [
    keyword: string | null | undefined,
    pageNumber: number | undefined,
    pageSize: number | undefined,
    sortBy: string | null | undefined,
    sortDirection: string | null | undefined] {
    return [this.keyword, this.pageNumber, this.pageSize, this.sortBy, this.sortDirection];
  }
}
