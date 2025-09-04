import { inject, Injectable } from '@angular/core';
import {
    IsProductCodeUniqueResponse,
    IsProductNameUniqueResponse,
    ProductsClient
} from '@api';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ValidatorsService {
    private readonly productsClient: ProductsClient = inject(ProductsClient);

    isCodeUnique = (id: string | undefined, code: string): Observable<IsProductCodeUniqueResponse> =>
        this.productsClient.isCodeUnique(id, code);

    isNameUnique = (id: string | undefined, name: string): Observable<IsProductNameUniqueResponse> =>
        this.productsClient.isNameUnique(id, name);
}
