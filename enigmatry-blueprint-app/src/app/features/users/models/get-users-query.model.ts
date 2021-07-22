import { UsersClient } from 'src/app/api/api-reference';
import { PagedQuery } from 'src/app/shared/query/paged-query.model';
import { ApiQuery } from 'src/app/shared/query/query.interface';

export class GetUsersQuery extends PagedQuery implements ApiQuery<UsersClient['search']> {

  getApiRequestParams(): [
    keyword: string | null | undefined,
    pageNumber: number | undefined,
    pageSize: number | undefined,
    sortBy: string | null | undefined,
    sortDirection: string | null | undefined] {
    return [this.keyword, this.pageNumber, this.pageSize, this.sortBy, this.sortDirection];
  }
}
