import { TextSearchFilter } from '@enigmatry/entry-components/search-filter';
import { SearchFilterPagedQuery } from 'src/app/shared/list-component/search-filter-paged-query';

export class GetUsersQuery extends SearchFilterPagedQuery {
  name = new TextSearchFilter({
    key: 'name',
    label: 'Name',
    placeholder: 'Name',
    maxLength: 25
  });

  email = new TextSearchFilter({
    key: 'email',
    label: 'E-mail',
    placeholder: 'user@example.com',
    type: 'email'
  });

  constructor() {
    super();
    this.filters = [this.email, this.name];
  }
}