import { SortDirection } from '@enigmatry/entry-table';
import { PagedQuery } from 'src/app/shared/query/paged-query.model';

export class GetProductsQuery extends PagedQuery {
    constructor(sortBy: string = 'price', sortDirection: SortDirection = 'asc') {
        super();
        this.sortBy = sortBy;
        this.sortDirection = sortDirection;
    }
}
