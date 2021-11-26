import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
    IsProductCodeUniqueResponse,
    IsProductNameUniqueResponse,
    ProductsClient
} from 'src/app/api/api-reference';

@Injectable({
    providedIn: 'root'
})
export class ValidatorsService {
    constructor(private productsClient: ProductsClient) { }

    isCodeUnique = (id: string | undefined, code: string): Observable<IsProductCodeUniqueResponse> =>
        this.productsClient.isCodeUnique(id, code);

    isNameUnique = (id: string | undefined, name: string): Observable<IsProductNameUniqueResponse> =>
        this.productsClient.isNameUnique(id, name);
}
