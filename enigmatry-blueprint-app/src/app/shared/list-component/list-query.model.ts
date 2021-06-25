import { Params } from '@angular/router';
import { PageEvent, SortDirection, SortEvent } from 'src/@enigmatry/pagination';
import { IListQuery, defaultPageSize } from './list-query.interface';

export class BaseListQuery implements IListQuery {

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

  getUrlQueryStringParams(): Params {
    return { ...this };
  }

  updateFromUrlQueryStringParams(params: Params): void {
    this.keyword = params.keyword;
    this.pageNumber = params.pageNumber ? Number(params.pageNumber) : 1;
    this.pageSize = params.pageSize ? Number(params.pageSize) : defaultPageSize;
    this.sortBy = params.sortBy;
    this.sortDirection = params.sortDirection;
  }
}
