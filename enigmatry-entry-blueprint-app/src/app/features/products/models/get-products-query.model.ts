/* eslint-disable @typescript-eslint/no-magic-numbers */
import { SelectFilterOption, SelectSearchFilter, TextSearchFilter } from '@enigmatry/entry-components/search-filter';
import { SortDirection } from '@enigmatry/entry-components/table';
import { addMonths } from 'date-fns';
import { SearchFilterPagedQuery } from 'src/app/shared/list-component/search-filter-paged-query';

export class GetProductsQuery extends SearchFilterPagedQuery {
  name = new TextSearchFilter({
    key: 'name',
    label: 'Product name',
    placeholder: 'Product name',
    maxLength: 25
  });

  code = new TextSearchFilter({
    key: 'code',
    label: 'Code',
    placeholder: 'Product code'
  });

  expiresInMonths = new SelectSearchFilter({
    key: 'expiresInMonths',
    label: 'Expires in',
    placeholder: 'Expires in',
    options: [
      new SelectFilterOption(1, '1 month'),
      new SelectFilterOption(3, '3 months'),
      new SelectFilterOption(6, '6 months')
    ]
  });

  constructor(sortBy: string = 'price', sortDirection: SortDirection = 'asc') {
    super();
    this.sortBy = sortBy;
    this.sortDirection = sortDirection;
    this.filters = [this.name, this.code, this.expiresInMonths];
  }

  get expiresBeforeDate(): Date | undefined {
    const expiresInMonths = this.expiresInMonths.value;
    const today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
    return expiresInMonths ? addMonths(today, expiresInMonths) : undefined;
  }
}
