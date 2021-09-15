/* eslint-disable @typescript-eslint/member-ordering */
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { OnPage, OnSort, PagedData, SortEvent, PageEvent } from '@enigmatry/angular-building-blocks/pagination';
import { Params } from '@angular/router';

export interface OnSelection<T> {
  selectionChange(selection: T[]): void;
}

/**
 * BaseListComponent
 * loads list data when page or sort changes
 */
export abstract class BaseListComponent<T, TQuery extends OnSort & OnPage>
  implements OnSort, OnPage, OnSelection<T> {
  /** BehaviorSubject that contains paged data */
  public readonly data = new BehaviorSubject<PagedData<T> | null>(null);

  /** BehaviorSubject that contains current selection */
  public readonly selection = new BehaviorSubject<T[]>([]);

  /** Query model */
  public query: TQuery;

  /** Whether data is being fetched */
  public loading: boolean;

  /** Fetch data function (needs to be implemented) */
  abstract fetchData(query: TQuery): Observable<PagedData<T>>;

  abstract createQueryInstance(routeParams: Params, queryParams: Params): TQuery;

  /** Load list and update the data subject with the latest value */
  loadList(): Subscription {
    this.loading = true;
    return this.fetchData(this.query)
      .pipe(finalize(() => {
        this.loading = false;
      }))
      .subscribe(response => this.data.next(response));
  }

  /** Handle sort change event */
  sortChange(sort: SortEvent): void {
    this.query.sortChange(sort);
    this.loadList();
  }

  /** Handle page change event */
  pageChange(page: PageEvent): void {
    this.query.pageChange(page);
    this.loadList();
  }

  /** Handle selection change event */
  selectionChange(selection: T[]): void {
    this.selection.next(selection);
  }
}
