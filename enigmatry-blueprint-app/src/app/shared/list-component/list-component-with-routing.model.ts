/* eslint-disable @typescript-eslint/member-ordering */
import { Router, ActivatedRoute, Params } from '@angular/router';
import { combineLatest, Subscription } from 'rxjs';
import { finalize, mergeMap, tap } from 'rxjs/operators';
import { OnSort, OnPage, PageEvent, SortEvent } from '@enigmatry/entry-table';
import { BaseListComponent } from './base-list-component.model';
import { PagedQuery } from '../query/paged-query.model';

/**
 * ListComponentWithRouting
 * subscribed to activatedRoute.params and activatedRoute.queryParams
 * and updates route query string on page and sort changes
 */
export abstract class ListComponentWithRouting<T, TQuery extends OnSort & OnPage & PagedQuery>
  extends BaseListComponent<T, TQuery> {
  protected router: Router;
  protected activatedRoute: ActivatedRoute;
  _shouldWatchRouteParams: boolean;

  watchRouteParams(router: Router, activatedRoute: ActivatedRoute): Subscription {
    this.router = router;
    this.activatedRoute = activatedRoute;
    this._shouldWatchRouteParams = true;

    return combineLatest([this.activatedRoute.params, this.activatedRoute.queryParams])
      .pipe(
        tap(params => {
          this.query = this.createQueryInstance(...params);
        }),
        mergeMap(() => {
          this.loading = true;
          return this.fetchData(this.query).pipe(finalize(() => {
            this.loading = false;
          }));
        })
      )
      .subscribe(response => this.data.next(response));
  }

  pageChange(page: PageEvent): void {
    this.query.pageChange(page);
    this.updateCurrentRoute(this.query.getRouteQueryParams());
  }

  sortChange(sort: SortEvent): void {
    this.query.sortChange(sort);
    this.updateCurrentRoute(this.query.getRouteQueryParams());
  }

  protected updateCurrentRoute(queryParams: Params): void {
    if (!this.router || !this.activatedRoute) {
      // eslint-disable-next-line no-console
      console.warn('Call to watchRouteParams in missing! Falling back to basic behavior.');
      this.loadList();
      return;
    }
    this.router.navigate([], { relativeTo: this.activatedRoute, queryParams, queryParamsHandling: 'merge' });
  }
}
