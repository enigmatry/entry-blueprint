// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------;
/* eslint-disable */
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { IGetProductDetailsResponse } from 'src/app/api/api-reference';
    import { map } from 'rxjs/operators';
    import { ProductEditLookupService } from '../services/product-edit-generated-lookup.service';


@Component({
  selector: 'app-g-product-edit',
  templateUrl: './product-edit-generated.component.html',
  styleUrls: ['./product-edit-generated.component.scss']
})
export class ProductEditGeneratedComponent implements OnInit {

  @Input() model: IGetProductDetailsResponse = {};
  @Input() set isReadonly(value: boolean) {
    this._isReadonly = value;
    this.fields = this.initializeFields();
  }
  get isReadonly() {
    return this._isReadonly;
  }

  @Output() save = new EventEmitter<IGetProductDetailsResponse>();
  @Output() cancel = new EventEmitter<void>();

  _isReadonly: boolean;
  form = new FormGroup({});
  fields: FormlyFieldConfig[] = [];

  constructor(private lookupService: ProductEditLookupService) {
    this.fields = this.initializeFields();
  }

  ngOnInit(): void {}

  onSubmit() {
    if (this.form.valid) {
      this.save.emit(this.model);
    }
  }

  initializeFields(): FormlyFieldConfig[] {
    return [
        {
        key: 'name',
        type: 'input',
        className: '',
        templateOptions: {
        label: $localize `:@@products.product-edit.name.label:Name`,
        placeholder: $localize `:@@products.product-edit.name.placeholder:Unique product name`,
        disabled: this.isReadonly || false,
        description: '',
        hidden: !true,
required: true,
minLength: 5,
maxLength: 50,
        },
modelOptions: { updateOn: 'blur' },
asyncValidators: { validation: [ 'productNameIsUnique' ] },
        },
        {
        key: 'code',
        type: 'input',
        className: '',
        templateOptions: {
        label: $localize `:@@products.product-edit.code.label:Code`,
        placeholder: $localize `:@@products.product-edit.code.placeholder:Unique product code identifier`,
        disabled: this.isReadonly || false,
        description: '',
        hidden: !true,
required: true,
pattern: /^[A-Z]{4}[1-9]{8}$/mu,
        },
            validation: {
            messages: {
pattern: $localize `:@@products.product-edit.code.pattern:Code must be in 4 letter 8 digits format (e.g. ABCD12345678)`
            }
            },
modelOptions: { updateOn: 'blur' },
asyncValidators: { validation: [ 'productCodeIsUnique' ] },
        },
        {
        key: 'type',
        type: 'select',
        className: '',
        templateOptions: {
        label: $localize `:@@products.product-edit.type.label:Type`,
        placeholder: $localize `:@@products.product-edit.type.placeholder:Type`,
        disabled: this.isReadonly || false,
        description: '',
            options: this.lookupService.getType$.pipe(
            map((arr) =>
            arr.map(el => el = {value: el.value, label: el.displayName}))
            ),
        hidden: !true,
required: true,
        },
        },
        {
        key: 'price',
        type: 'input',
        className: '',
        templateOptions: {
        label: $localize `:@@products.price:Price per unit`,
        placeholder: $localize `:@@products.price:Price per unit`,
        disabled: this.isReadonly || false,
        description: '',
        hidden: !true,
required: true,
type: 'number',
min: 0.99 + 0.1,
max: 999.99,
        },
        },
        {
        key: 'amount',
        type: 'input',
        className: '',
        templateOptions: {
        label: $localize `:@@products.amount:Units`,
        placeholder: $localize `:@@products.amount:Units`,
        disabled: this.isReadonly || false,
        description: '',
        hidden: !true,
required: true,
type: 'number',
min: 0 + 1,
max: 100,
        },
        },
        {
        key: 'contactEmail',
        type: 'input',
        className: '',
        templateOptions: {
        label: $localize `:@@products.product-edit.contact-email.label:Contact email`,
        placeholder: $localize `:@@products.product-edit.contact-email.placeholder:Contact person email address`,
        disabled: this.isReadonly || false,
        description: '',
        hidden: !true,
required: true,
pattern: /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/,
        },
            validation: {
            messages: {
pattern: $localize `:@@validators.pattern.emailAddress:Invalid email address format`
            }
            },
        },
        {
        key: 'contactPhone',
        type: 'input',
        className: '',
        templateOptions: {
        label: $localize `:@@products.product-edit.contact-phone.label:Contact phone`,
        placeholder: $localize `:@@products.product-edit.contact-phone.placeholder:Contact person phone number`,
        disabled: this.isReadonly || false,
        description: '',
        hidden: !true,
required: true,
pattern: /^s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$/mu,
        },
        },
        {
        key: 'infoLink',
        type: 'input',
        className: '',
        templateOptions: {
        label: $localize `:@@products.product-edit.info-link.label:Homepage`,
        placeholder: $localize `:@@products.product-edit.info-link.placeholder:Link to product homepage`,
        disabled: this.isReadonly || false,
        description: '',
        hidden: !true,
        },
modelOptions: { updateOn: 'blur' },
asyncValidators: { validation: [ 'isLink' ] },
        },
        {
        key: 'expiresOn',
        type: 'datepicker',
        className: '',
        templateOptions: {
        label: $localize `:@@products.product-edit.expires-on.label:Expires on`,
        placeholder: $localize `:@@products.product-edit.expires-on.placeholder:Product expiration date if any`,
        disabled: this.isReadonly || false,
        description: '',
        hidden: !true,
        },
        },
        {
        key: 'freeShipping',
        type: this.isReadonly ? 'readonly-boolean' : 'checkbox',
        className: '',
        templateOptions: {
        label: $localize `:@@products.product-edit.free-shipping.label:Free shipping`,
        placeholder: $localize `:@@products.product-edit.free-shipping.placeholder:Free shipping`,
        disabled: this.isReadonly || false,
        description: '',
        hidden: !true,
        },
        },
    ];
  }
}