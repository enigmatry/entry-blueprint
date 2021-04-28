import { Params } from '@angular/router';

export interface PagedResponse<T> {
    items?: T[];
    pageSize?: number;
    pageNumber?: number;
    totalCount?: number;
    totalPages?: number;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;
}

export class PagedQueryParams {

    keyword?: string;
    pageNumber = 1;
    pageSize = 10;
    sortBy?: string;
    sortDirection?: 'asc' | 'desc' | '';

    constructor() { }

    update(queryParams: Params): void {
        this.keyword = queryParams.keyword;
        this.pageNumber = Number.isSafeInteger(queryParams.pageNumber) ? Number(queryParams.pageNumber) : this.pageNumber;
        this.pageSize = Number.isSafeInteger(queryParams.pageSize) ? Number(queryParams.pageSize) : this.pageSize;
        this.sortBy = queryParams.sortBy ?? this.sortBy;
        this.sortDirection = queryParams.sortDirection ?? this.sortDirection;
    }
}
