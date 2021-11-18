/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable unused-imports/no-unused-vars */
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { GetProductDetailsResponse, ProductsClient } from 'src/app/api/api-reference';

@Injectable({
    providedIn: 'root'
})
// eslint-disable-next-line id-length
export class ProductResolverService implements Resolve<GetProductDetailsResponse> {
    constructor(private client: ProductsClient) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<GetProductDetailsResponse> {
        return this.client.get(route.params.id);
    }
}