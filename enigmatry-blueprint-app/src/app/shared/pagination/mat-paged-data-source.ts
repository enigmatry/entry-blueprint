import { DataSource } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { PagedResponse, PagedQueryParams } from './paged-data.model';

export class MatPagedDataSource<T> implements DataSource<T> {

  paginator: MatPaginator;
  sort: MatSort;
  router: Router;
  activatedRoute: ActivatedRoute;
  queryParams: PagedQueryParams;

  private _pagedResponse = new BehaviorSubject<PagedResponse<T>>({ items: [] });

  get pagedResponse() {
    return this._pagedResponse.value;
  }
  set pagedResponse(response: PagedResponse<T>) {
    this._pagedResponse.next(response);
  }

  get totalRecords() {
    return this.pagedResponse?.totalCount;
  }

  private destroy$: Subject<boolean> = new Subject<boolean>();

  constructor() { }

  connect(): Observable<T[]> {

    this.sort.sortChange.pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.paginator.pageIndex = 0;
        this.onPageOrSortChange();
      });

    this.paginator.page.pipe(takeUntil(this.destroy$))
      .subscribe(() => this.onPageOrSortChange());

    return this._pagedResponse.pipe(map(response => response.items ?? []));
  }

  disconnect(): void {
    this.destroy$.next(true);
  }

  onPageOrSortChange() {
    if (this.paginator) {
      this.queryParams.pageNumber = this.paginator.pageIndex + 1;
      this.queryParams.pageSize = this.paginator.pageSize;
    }
    if (this.sort.active) {
      this.queryParams.sortBy = this.sort.active;
      this.queryParams.sortDirection = this.sort.direction;
    }
    this.updateRouteQueryParamsWithoutChangingCurrentRoute();
  }

  private updateRouteQueryParamsWithoutChangingCurrentRoute() {
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: this.queryParams,
      queryParamsHandling: 'merge'
    });
  }
}
