import { UsersClient } from 'src/app/api/api-reference';
import { BaseListQuery, IApiMethodParams } from 'src/app/shared/list-component/list-query.model';

export class GetUsersQuery extends BaseListQuery implements IApiMethodParams<UsersClient['search']> {

  getApiMethodParams(): [
    keyword: string | null | undefined,
    pageNumber: number | undefined,
    pageSize: number | undefined,
    sortBy: string | null | undefined,
    sortDirection: string | null | undefined] {
    return [this.keyword, this.pageNumber, this.pageSize, this.sortBy, this.sortDirection];
  }
}
