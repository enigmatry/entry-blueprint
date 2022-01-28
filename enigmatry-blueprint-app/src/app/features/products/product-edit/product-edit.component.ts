/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable unused-imports/no-unused-vars */
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    IGetProductDetailsResponse,
    IProductCreateOrUpdateCommand,
    ProductCreateOrUpdateCommand,
    ProductsClient,
    ProductType
} from 'src/app/api/api-reference';
import { FormAccessMode } from 'src/app/shared/form-component/form-access-mode.enum';
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
        this.initDisableExpressions();
        this.initRequiredExpressions();
        this.initPropertyExpressions();
    }

    save = (model: IProductCreateOrUpdateCommand) =>
        this.client
            .post(model as ProductCreateOrUpdateCommand)
            .subscribe(this.goBack);

    buttonClick = (name: string) => {
        if (name === 'resetFormBtn') {
          this.resetModel();
        }
    };

    toCommand(response: IGetProductDetailsResponse): IProductCreateOrUpdateCommand {
        return new ProductCreateOrUpdateCommand({
            id: response.id,
            name: response.name,
            code: response.code,
            type: response.type,
            description: response.description,
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
            (model: IGetProductDetailsResponse): boolean =>
                model.hasDiscount === undefined || model.hasDiscount === false;
        this.fieldsHideExpressions.resetFormBtn =
            (model: IGetProductDetailsResponse): boolean =>
                !this.isEdit();
    };

    private initDisableExpressions = () => {
        this.fieldsDisableExpressions.price =
            (model: IGetProductDetailsResponse): boolean =>
                model.type === undefined ||
                model.type === ProductType.Book && this.formMode === FormAccessMode.edit;
      this.fieldsDisableExpressions.expiresOn =
          (model: IGetProductDetailsResponse): boolean =>
            model.type === ProductType.Car || model.type === ProductType.Book;
    };

    private initRequiredExpressions = () => {
      this.fieldsRequiredExpressions.discount =
          (model: IGetProductDetailsResponse): boolean => !!model.hasDiscount;
      this.fieldsRequiredExpressions.expiresOn =
          (model: IGetProductDetailsResponse): boolean =>
            model.type === ProductType.Drink || model.type === ProductType.Food;
    };

    private initPropertyExpressions = () => {
      this.fieldsPropertyExpressions.expiresOn =
          (model: IGetProductDetailsResponse): Date | undefined =>
            model.type === ProductType.Car || model.type === ProductType.Book ? undefined : model.expiresOn;
    };
}
