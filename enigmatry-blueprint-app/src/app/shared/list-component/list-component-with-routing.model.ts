import { Router, ActivatedRoute } from '@angular/router';
import { combineLatest } from 'rxjs';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { tap, mergeMap, finalize, map } from 'rxjs/operators';
import { PagedData, PageEvent, SortEvent } from 'src/@enigmatry/pagination';
import { IListComponent } from './list-component.interface';
import { IListQueryWithRouting } from './list-query.model';

export class ListComponentWithRouting<T, TQuery extends IListQueryWithRouting> implements IListComponent<T, TQuery> {

  data = new BehaviorSubject<PagedData<T> | null>(null);
  selection = new BehaviorSubject<T[]>([]);
  query: TQuery;

  loading: boolean;
  fetchData: (query: TQuery) => Observable<PagedData<T>>;

  protected router: Router;
  protected activatedRoute: ActivatedRoute;
  private shouldWatchRouteParams: boolean;

  watchRouteParams(router: Router, activatedRoute: ActivatedRoute): Subscription {
    this.router = router;
    this.activatedRoute = activatedRoute;
    this.shouldWatchRouteParams = true;

    return combineLatest([this.activatedRoute.params, this.activatedRoute.queryParams])
      .pipe(
        tap(res => this.query.routeChanges(res[0], res[1])),
        mergeMap(() => {
          this.loading = true;
          return this.fetchData(this.query).pipe(finalize(() => this.loading = false));
        })
      )
      .subscribe(response => this.data.next(response));
  }

  pageChange(page: PageEvent): void {
    this.query.pageChange(page);
    this.updateRouteQueryString(this.shouldWatchRouteParams);
  }

  sortChange(sort: SortEvent): void {
    this.query.sortChange(sort);
    this.updateRouteQueryString(this.shouldWatchRouteParams);
  }

  selectionChange(selection: T[]): void {
    this.selection.next(selection);
  }

  private updateRouteQueryString(condition: boolean): void {
    if (!condition) { return; }
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: this.query.getRouteQueryParams(),
      queryParamsHandling: 'merge'
    });
  }
}
