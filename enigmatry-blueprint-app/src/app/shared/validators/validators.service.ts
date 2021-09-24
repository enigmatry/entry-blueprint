import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
    GetProductCodeUniquenessResponse,
    GetProductNameUniquenessResponse,
    ProductsClient
} from 'src/app/api/api-reference';

@Injectable({
    providedIn: 'root'
})
export class ValidatorsService {
    constructor(private productsClient: ProductsClient) { }

    isCodeUnique = (code: string): Observable<GetProductCodeUniquenessResponse> =>
        this.productsClient.isCodeUnique(code);

    isNameUnique = (code: string): Observable<GetProductNameUniquenessResponse> =>
        this.productsClient.isNameUnique(code);
}
