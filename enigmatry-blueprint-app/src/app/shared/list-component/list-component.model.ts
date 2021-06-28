import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { PagedData, PageEvent, SortEvent } from 'src/@enigmatry/pagination';
import { IListComponent } from './list-component.interface';
import { IListQuery } from './list-query.model';

export class ListComponent<T, TQuery extends IListQuery> implements IListComponent<T, TQuery> {

  data = new BehaviorSubject<PagedData<T> | null>(null);
  selection = new BehaviorSubject<T[]>([]);

  query: TQuery;

  loading = false;
  fetchData: (query: TQuery) => Observable<PagedData<T>>;

  refreshList(): Subscription {
    this.loading = true;

    return this.fetchData(this.query)
      .pipe(finalize(() => this.loading = false))
      .subscribe(response => this.data.next(response));
  }

  sortChange(sort: SortEvent): void {
    this.query.sortChange(sort);
    this.refreshList();
  }

  pageChange(page: PageEvent): void {
    this.query.pageChange(page);
    this.refreshList();
  }

  selectionChange(selection: T[]): void {
    this.selection.next(selection);
  }

}
