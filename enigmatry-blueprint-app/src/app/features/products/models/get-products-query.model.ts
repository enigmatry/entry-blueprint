import { TextSearchFilter } from '@enigmatry/entry-components/search-filter';
import { SortDirection } from '@enigmatry/entry-table';
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

  contactEmail = new TextSearchFilter({
    key: 'contactEmail',
    label: 'Contact E-mail',
    placeholder: 'user@example.com',
    type: 'email'
  });

  constructor(sortBy: string = 'price', sortDirection: SortDirection = 'asc') {
    super();
    this.sortBy = sortBy;
    this.sortDirection = sortDirection;
    this.filters = [this.name, this.code, this.contactEmail];
  }
}
