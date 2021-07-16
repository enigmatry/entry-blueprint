import { Params } from '@angular/router';
import { PageEvent, SortDirection, SortEvent } from 'src/@enigmatry/pagination';
import { OnPage, OnSort } from './list-component.interface';

export const defaultPageSize = 10;

export interface IListQuery extends OnPage, OnSort { }

export interface IListQueryWithRouting extends IListQuery {
  routeChanges(routeParams: Params, queryParams: Params): void;
  getRouteQueryParams(): Params;
}

export interface IApiMethodParams<TFunction extends (...args: any) => any> {
  getApiMethodParams(): Parameters<TFunction>;
}

export class BaseListQuery implements IListQueryWithRouting {

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

  routeChanges(routeParams: Params, queryParams: Params): void {
    this.keyword = queryParams.keyword;
    this.pageNumber = queryParams.pageNumber ? Number(queryParams.pageNumber) : 1;
    this.pageSize = queryParams.pageSize ? Number(queryParams.pageSize) : defaultPageSize;
    this.sortBy = queryParams.sortBy;
    this.sortDirection = queryParams.sortDirection;
  }

  getRouteQueryParams(): Params {
    return { ...this }; // all properties are added to query params
  }
}
