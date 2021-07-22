import { Params } from '@angular/router';

/**
 * Represents a query that is sent to the API and provides request params for the api method call.
 */
export interface ApiQuery<TFunc extends (...args: any) => any> {
    getApiRequestParams(): Parameters<TFunc>;
}

/**
 * Represents a query that is aware of the current route :
 * - reads values from routeParams or queryParams
 * - can have values that are serialized into route query string
 */
export interface RouteAwareQuery {
    applyRouteChanges(routeParams: Params, queryParams: Params): void;
    getRouteQueryParams(): Params;
}
