import { Params } from '@angular/router';
import { SearchFilterBase, SearchFilterParams } from '@enigmatry/entry-components/search-filter';
import { PagedQuery } from '@enigmatry/entry-components/table';

export class SearchFilterPagedQuery extends PagedQuery {
  // Capture parent field implementations before child overrides replace them
  private readonly _parentApplyRouteChanges = this.applyRouteChanges;
  private readonly _parentGetRouteQueryParams = this.getRouteQueryParams;

  constructor(public filters: SearchFilterBase<any>[] = []) {
    super();
  }

  searchFilterChange(searchParams: SearchFilterParams): void {
    this.filters.forEach(filter => filter.setValue(searchParams[filter.key]));
    this.pageNumber = 1;
  }

  override readonly applyRouteChanges = (queryParams: Params): void => {
    this._parentApplyRouteChanges(queryParams);
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

  isFilterValueEmpty = (filterValue: any): boolean => filterValue === undefined
      || filterValue === null
      || filterValue?.length === 0
      || filterValue[0] === undefined;

  override readonly getRouteQueryParams = (): Params => {
    const pagedParams = this._parentGetRouteQueryParams();
    const filterParams: Params = {};
    this.filters.forEach(filter => filterParams[filter.key] = filter.value);
    return {
      ...pagedParams,
      ...filterParams
    };
  }
}
