import { Params } from '@angular/router';
import { SortDirection } from 'src/@enigmatry/pagination';
import { OnPage, OnSort } from './list-component.interface';

export const defaultPageSize = 10;

export interface IPageQuery extends OnPage, OnSort { }

export interface IPageQueryWithRouthing extends IPageQuery {
  getUrlQueryStringParams(): Params;
  updateFromUrlQueryStringParams(params: Params): void;
}

export interface IListQuery extends IPageQueryWithRouthing {
  keyword?: string;
  pageNumber: number;
  pageSize: number;
  sortBy?: string;
  sortDirection?: SortDirection;
}

export interface IHasApiMethodParams<TFunction extends (...args: any) => any> {
  getApiMethodParams(): Parameters<TFunction>;
}
