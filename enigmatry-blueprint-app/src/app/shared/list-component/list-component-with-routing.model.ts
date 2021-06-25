import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { tap, mergeMap, finalize } from 'rxjs/operators';
import { PagedData, PageEvent, SortEvent } from 'src/@enigmatry/pagination';
import { IListComponent } from './list-component.interface';
import { IPageQueryWithRouthing } from './list-query.interface';

export class ListComponentWithRouting<T, TQuery extends IPageQueryWithRouthing> implements IListComponent<T, TQuery> {

  data = new BehaviorSubject<PagedData<T> | null>(null);
  selection = new BehaviorSubject<T[]>([]);
  query: TQuery;

  loading: boolean;
  fetchData: (query: TQuery) => Observable<PagedData<T>>;

  protected router: Router;
  protected activatedRoute: ActivatedRoute;
  private shouldWatchRouteQueryString: boolean;

  watchRouteQueryParams(router: Router, activatedRoute: ActivatedRoute): Subscription {
    this.router = router;
    this.activatedRoute = activatedRoute;
    this.shouldWatchRouteQueryString = true;

    return activatedRoute.queryParams
      .pipe(
        tap(params => this.query.updateFromUrlQueryStringParams(params)),
        mergeMap(() => {
          this.loading = true;
          return this.fetchData(this.query).pipe(finalize(() => this.loading = false));
        })
      )
      .subscribe(response => this.data.next(response));
  }

  pageChange(page: PageEvent): void {
    this.query.pageChange(page);
    this.updateRouteQueryString(this.shouldWatchRouteQueryString);
  }

  sortChange(sort: SortEvent): void {
    this.query.sortChange(sort);
    this.updateRouteQueryString(this.shouldWatchRouteQueryString);
  }

  selectionChange(selection: T[]): void {
    this.selection.next(selection);
  }

  private updateRouteQueryString(condition: boolean): void {
    if (!condition) { return; }
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: this.query.getUrlQueryStringParams(),
      queryParamsHandling: 'merge'
    });
  }
}
