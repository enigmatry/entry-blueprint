import { BehaviorSubject, Observable } from 'rxjs';
import { PagedData, PageEvent, SortEvent } from '@enigmatry/pagination';

export interface OnSort {
  sortChange(sort: SortEvent): void;
}

export interface OnPage {
  pageChange(page: PageEvent): void;
}

export interface OnSelection<T> {
  selectionChange(selection: T[]): void;
}

export interface IListComponent<T, TQuery> extends OnSort, OnPage, OnSelection<T> {
  data: BehaviorSubject<PagedData<T> | null>;
  selection: BehaviorSubject<T[]>;
  query: TQuery;

  loading: boolean;
  fetchData(query: TQuery): Observable<PagedData<T>>;
}
