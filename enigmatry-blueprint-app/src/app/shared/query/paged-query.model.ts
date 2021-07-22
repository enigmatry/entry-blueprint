import { Params } from '@angular/router';
import { OnPage, OnSort, PageEvent, SortDirection, SortEvent } from 'src/@enigmatry/pagination';
import { RouteAwareQuery } from './query.interface';

export const defaultPageSize = 10;

export class PagedQuery implements OnPage, OnSort, RouteAwareQuery {
  keyword?: string;
  pageNumber = 1;
  pageSize = defaultPageSize;
  sortBy?: string;
  sortDirection?: SortDirection;

  sortChange(sort: SortEvent): void {
    if (sort.active) {
      this.sortBy = sort.active;
      this.sortDirection = sort.direction;
      this.pageNumber = 1;
    }
  }

  pageChange(page: PageEvent): void {
    this.pageNumber = page.pageIndex + 1;
    this.pageSize = page.pageSize;
  }

  applyRouteChanges(routeParams: Params, queryParams: Params): void {
    this.keyword = queryParams.keyword;
    this.pageNumber = queryParams.pageNumber ? Number(queryParams.pageNumber) : 1;
    this.pageSize = queryParams.pageSize ? Number(queryParams.pageSize) : defaultPageSize;
    this.sortBy = queryParams.sortBy;
    this.sortDirection = queryParams.sortDirection;
  }

  getRouteQueryParams(): Params {
    return { ...this }; // all properties are added to query params
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
