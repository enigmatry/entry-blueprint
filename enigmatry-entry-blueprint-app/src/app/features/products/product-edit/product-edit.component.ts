import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  IGetProductDetailsResponse,
  IProductCreateOrUpdateCommand,
  PermissionId,
  ProductCreateOrUpdateCommand,
  ProductsClient,
  ProductType
} from '@api';
import { IValidationProblemDetails, setServerSideValidationErrors } from '@enigmatry/entry-components/validation';
import { FormAccessMode } from '@shared/form-component/form-access-mode.enum';
import { FormComponent } from '@shared/form-component/form-component.model';
import { ProductEditGeneratedComponent } from '../generated/product-edit/product-edit-generated.component';

@Component({
  standalone: false,
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent
  extends FormComponent<IProductCreateOrUpdateCommand, IGetProductDetailsResponse> {
  @ViewChild('formComponent') formComponent: ProductEditGeneratedComponent;

  PermissionId = PermissionId;

  constructor(
    protected override router: Router,
    protected override activatedRoute: ActivatedRoute,
    private client: ProductsClient) {
    super(router, activatedRoute);
    this.initHideExpressions();
    this.initDisableExpressions();
    this.initRequiredExpressions();
    this.initPropertyExpressions();
  }

  save = (model: IGetProductDetailsResponse) =>
    this.client
      .post(this.toCommand(model))
      .subscribe({
        next: () => this.goBack(),
        error: (error: IValidationProblemDetails) =>
          setServerSideValidationErrors(error, this.formComponent.form)
      });

  buttonClick = (name: string) => {
    if (name === 'resetFormBtn') {
      this.resetModel();
    }
  };

  toCommand = (response: IGetProductDetailsResponse): ProductCreateOrUpdateCommand => new ProductCreateOrUpdateCommand({
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

  private initHideExpressions = () => {
    this.fieldsHideExpressions['discount'] =
      (model: IGetProductDetailsResponse): boolean =>
        model.hasDiscount === undefined || model.hasDiscount === false;
    this.fieldsHideExpressions['resetFormBtn'] =
      (_model: IGetProductDetailsResponse): boolean =>
        !this.isEdit();
  };

  private initDisableExpressions = () => {
    this.fieldsDisableExpressions['price'] =
      (model: IGetProductDetailsResponse): boolean =>
        model.type === undefined ||
        model.type === ProductType.Book && this.formMode === FormAccessMode.edit;
    this.fieldsDisableExpressions['expiresOn'] =
      (model: IGetProductDetailsResponse): boolean =>
        model.type === ProductType.Car || model.type === ProductType.Book;
  };

  private initRequiredExpressions = () => {
    this.fieldsRequiredExpressions['discount'] =
      (model: IGetProductDetailsResponse): boolean => !!model.hasDiscount;
    this.fieldsRequiredExpressions['expiresOn'] =
      (model: IGetProductDetailsResponse): boolean =>
        model.type === ProductType.Drink || model.type === ProductType.Food;
  };

  private initPropertyExpressions = () => {
    this.fieldsPropertyExpressions['expiresOn'] =
      (model: IGetProductDetailsResponse): Date | undefined =>
        model.type === ProductType.Car || model.type === ProductType.Book ? undefined : model.expiresOn;
  };
}
