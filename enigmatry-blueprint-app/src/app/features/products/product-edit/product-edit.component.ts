/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable unused-imports/no-unused-vars */
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    IGetProductDetailsResponse,
    IProductCreateOrUpdateCommand,
    ProductCreateOrUpdateCommand,
    ProductsClient
} from 'src/app/api/api-reference';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { FormComponent } from 'src/app/shared/form-component/form-component.model';

@Component({
    selector: 'app-product-edit',
    templateUrl: './product-edit.component.html',
    styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent
    extends FormComponent<IProductCreateOrUpdateCommand, IGetProductDetailsResponse> {
    constructor(
        protected router: Router,
        protected activatedRoute: ActivatedRoute,
        private client: ProductsClient) {
        super(router, activatedRoute);
        this.saveText = $localize`:@@common.save-product:Save product`;
        this.initHideExpressions();
    }

    save = (model: IProductCreateOrUpdateCommand) =>
        this.client
            .post(model as ProductCreateOrUpdateCommand)
            .subscribe(this.goBack);

    toCommand(response: IGetProductDetailsResponse): IProductCreateOrUpdateCommand {
        return new ProductCreateOrUpdateCommand({
            id: response.id,
            name: response.name,
            code: response.code,
            type: response.type,
            price: response.price,
            contactEmail: response.contactEmail,
            contactPhone: response.contactPhone,
            infoLink: response.infoLink,
            amount: response.amount,
            expiresOn: response.expiresOn,
            freeShipping: response.freeShipping,
            hasDiscount: response.hasDiscount,
            discount: response.discount
        });
    }

    private initHideExpressions = () => {
        this.fieldsHideExpressions.discount =
            (model: IGetProductDetailsResponse, formState: any, field: FormlyFieldConfig | undefined)
            : boolean => model.hasDiscount === false;
    };
}
