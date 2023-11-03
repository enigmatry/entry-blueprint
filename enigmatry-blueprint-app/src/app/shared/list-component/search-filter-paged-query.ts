import { Params } from '@angular/router';
import { SearchFilterBase, SearchFilterParams } from '@enigmatry/entry-components/search-filter';
import { PagedQuery } from '@enigmatry/entry-components/table';

export class SearchFilterPagedQuery extends PagedQuery {
  constructor(public filters: SearchFilterBase<any>[] = []) {
    super();
  }

  searchFilterChange(searchParams: SearchFilterParams): void {
    this.filters.forEach(filter => filter.setValue(searchParams[filter.key]));
    this.pageNumber = 1;
  }

  applyRouteChanges(queryParams: Params): void {
    super.applyRouteChanges(queryParams);
    this.filters.forEach(filter => {
      const value = this.getValueFromQueryParam(queryParams, filter.key);
      filter.setValue(value);
    });
  }

  private getValueFromQueryParam(queryParams: Params, filterKey: string): string | number | null | undefined {
    const filterValue = queryParams[filterKey];
    if (this.isFilterValueEmpty(filterValue)) {
      return filterValue;
    }

    const parsed = Number(filterValue);
    if (!isNaN(parsed)) {
      return parsed;
    }

    return filterValue;
  }

  private isFilterValueEmpty(filterValue: any): boolean {
    return filterValue === undefined
      || filterValue === null
      || filterValue?.length === 0
      || filterValue[0] === undefined;
  }

  override getRouteQueryParams(): Params {
    const pagedParams = super.getRouteQueryParams();
    const filterParams: Params = {};
    this.filters.forEach(filter => filterParams[filter.key] = filter.value);
    return {
      ...pagedParams,
      ...filterParams
    };
  }
}
