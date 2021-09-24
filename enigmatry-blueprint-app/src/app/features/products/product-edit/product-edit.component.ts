import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Location } from '@angular/common';
import { IGetProductDetailsResponse, ProductCreateOrUpdateCommand, ProductsClient } from 'src/app/api/api-reference';

@Component({
    selector: 'app-product-edit',
    templateUrl: './product-edit.component.html',
    styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {
    model: IGetProductDetailsResponse = {};

    constructor(private client: ProductsClient, protected activatedRoute: ActivatedRoute, public location: Location) { }

    ngOnInit(): void {
        this.activatedRoute.params
            .pipe(switchMap(params => this.client.get(params.id)))
            .subscribe(response => {
                this.model = response;
            });
    }

    save(model: IGetProductDetailsResponse) {
        const command = new ProductCreateOrUpdateCommand({
            id: model.id,
            name: model.name,
            code: model.code,
            type: model.type,
            price: model.price,
            contactEmail: model.contactEmail,
            contactPhone: model.contactPhone,
            expiresOn: model.expiresOn
        });
        this.client.post(command)
            .subscribe(() => this.location.back());
    }

    cancel() {
        this.location.back();
    }
}
