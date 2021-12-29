// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------;
/* eslint-disable */
import { Component, EventEmitter, Inject, Input, OnInit, Optional, Output, TemplateRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { IGetProductDetailsResponse } from 'src/app/api/api-reference';
import { IFieldExpressionDictionary, SelectConfiguration, ENIGMATRY_FIELD_TYPE_RESOLVER, FieldTypeResolver } from '@enigmatry/angular-building-blocks/form';
import { BehaviorSubject } from 'rxjs';


@Component({
  selector: 'app-g-product-edit',
  templateUrl: './product-edit-generated.component.html',
  styleUrls: ['./product-edit-generated.component.scss']
})
export class ProductEditGeneratedComponent implements OnInit {

  @Input() model: IGetProductDetailsResponse = {} as IGetProductDetailsResponse;
  @Input() set isReadonly(value: boolean) {
    this._isReadonly = value;
    this.fields = this.initializeFields();
  }
  get isReadonly() {
    return this._isReadonly;
  }

  @Input() saveButtonText: string = 'Save';
  @Input() cancelButtonText: string = 'Cancel';
  @Input() formButtonsTemplate: TemplateRef<any> | null | undefined;

  @Input() fieldsHideExpressions: IFieldExpressionDictionary<IGetProductDetailsResponse> | undefined = undefined;
  @Input() fieldsDisableExpressions: IFieldExpressionDictionary<IGetProductDetailsResponse> | undefined = undefined;

  @Output() save = new EventEmitter<IGetProductDetailsResponse>();
  @Output() cancel = new EventEmitter<void>();

                @Input() typeOptions: any[] = [{ value: 0, displayName: $localize `:@@enum.product-type.food:Food` }, { value: 1, displayName: $localize `:@@enum.product-type.drink:Drink` }, { value: 2, displayName: $localize `:@@enum.product-type.book:Book` }, { value: 3, displayName: $localize `:@@enum.product-type.car:Car` }];
                @Input() typeOptionsConfiguration: SelectConfiguration = { valueProperty: 'value', labelProperty: 'displayName' };

 _isReadonly: boolean;
  form = new FormGroup({});
  fields: FormlyFieldConfig[] = [];

  constructor(@Optional() @Inject(ENIGMATRY_FIELD_TYPE_RESOLVER) private _fieldTypeResolver: FieldTypeResolver) { }

  ngOnInit(): void {
    this.fields = this.initializeFields();
  }

  onSubmit() {
    if (this.form.valid) {
      this.save.emit(this.model);
    }
  }

  resolveFieldType = (type: string, isControlReadonly: boolean): string =>
    this._fieldTypeResolver ? this._fieldTypeResolver(type, this.isReadonly || isControlReadonly) : type;

  initializeFields(): FormlyFieldConfig[] {
    return [
            { key: 'id' },
        {
        key: 'name',
        type: this.resolveFieldType('input', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.name ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.name ? this.fieldsDisableExpressions.name(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.name.label:Name`,
        placeholder: $localize `:@@products.product-edit.name.placeholder:Unique product name`,
        description: '',
            appearance: 'outline',
        hidden: !true,
            required: true,
minLength: 5,
maxLength: 50,

            typeFormatDef: undefined
        },
modelOptions: { updateOn: 'blur' },
asyncValidators: { validation: [ 'productNameIsUnique' ] },
        },
        {
        key: 'code',
        type: this.resolveFieldType('input', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.code ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.code ? this.fieldsDisableExpressions.code(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.code.label:Code`,
        placeholder: $localize `:@@products.product-edit.code.placeholder:Unique product code identifier`,
        description: '',
        hidden: !true,
            required: true,
pattern: /^[A-Z]{4}[1-9]{8}$/mu,

            typeFormatDef: undefined
        },
            validation: {
            messages: {
pattern: (err, field) => $localize `:@@products.product-edit.code.pattern:Code must be in 4 letter 8 digits format (e.g. ABCD12345678)`
            }
            },
modelOptions: { updateOn: 'blur' },
asyncValidators: { validation: [ 'productCodeIsUnique' ] },
        },
        {
        key: 'type',
        type: this.resolveFieldType('autocomplete', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.type ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.type ? this.fieldsDisableExpressions.type(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.type.label:Type`,
        placeholder: $localize `:@@products.product-edit.type.placeholder:Type`,
        description: '',
            options: this.typeOptions,
            valueProp: this.typeOptionsConfiguration.valueProperty,
            labelProp: this.typeOptionsConfiguration.labelProperty,
        hidden: !true,
            required: true,

            typeFormatDef: undefined
        },
        },
        {
        key: 'description',
        type: this.resolveFieldType('textarea', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.description ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.description ? this.fieldsDisableExpressions.description(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.description.label:Description`,
        placeholder: $localize `:@@products.product-edit.description.placeholder:Description`,
        description: '',
            rows: 2,
            cols: 0,
        hidden: !true,
            typeFormatDef: undefined
        },
        },
        {
        key: 'price',
        type: this.resolveFieldType('input', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.price ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.price ? this.fieldsDisableExpressions.price(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.price:Price per unit`,
        placeholder: $localize `:@@products.price:Price per unit`,
        description: '',
        hidden: !true,
            required: true,
type: 'number',
min: 0.99 + 0.1,
max: 999.99,

            typeFormatDef: { name: 'currency', currencyCode: 'EUR', display: '€', digitsInfo: '', locale: '' }
        },
        },
        {
        key: 'amount',
        type: this.resolveFieldType('input', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.amount ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.amount ? this.fieldsDisableExpressions.amount(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.amount:Units`,
        placeholder: $localize `:@@products.amount:Units`,
        description: '',
        hidden: !true,
            required: true,
type: 'number',
min: 0 + 1,
max: 100,

            typeFormatDef: undefined
        },
        },
        {
        key: 'contactEmail',
        type: this.resolveFieldType('input', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.contactEmail ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.contactEmail ? this.fieldsDisableExpressions.contactEmail(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.contact-email.label:Contact email`,
        placeholder: $localize `:@@products.product-edit.contact-email.placeholder:Contact person email address`,
        description: '',
        hidden: !true,
            required: true,
pattern: /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/,

            typeFormatDef: undefined
        },
            validation: {
            messages: {
pattern: (err, field) => $localize `:@@validators.pattern.emailAddress:Invalid email address format`
            }
            },
        },
        {
        key: 'contactPhone',
        type: this.resolveFieldType('input', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.contactPhone ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.contactPhone ? this.fieldsDisableExpressions.contactPhone(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.contact-phone.label:Contact phone`,
        placeholder: $localize `:@@products.product-edit.contact-phone.placeholder:Contact person phone number`,
        description: '',
        hidden: !true,
            required: true,
pattern: /^s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$/mu,

            typeFormatDef: undefined
        },
        },
        {
        key: 'infoLink',
        type: this.resolveFieldType('input', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.infoLink ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.infoLink ? this.fieldsDisableExpressions.infoLink(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.info-link.label:Homepage`,
        placeholder: $localize `:@@products.product-edit.info-link.placeholder:Link to product homepage`,
        description: '',
        hidden: !true,
            pattern: /https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)/u,

            typeFormatDef: undefined
        },
        },
        {
        key: 'expiresOn',
        type: this.resolveFieldType('datepicker', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.expiresOn ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.expiresOn ? this.fieldsDisableExpressions.expiresOn(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.expires-on.label:Expires on`,
        placeholder: $localize `:@@products.product-edit.expires-on.placeholder:Product expiration date, if any`,
        description: '',
        hidden: !true,
            typeFormatDef: { name: 'date' }
        },
modelOptions: { updateOn: 'blur' },
asyncValidators: { validation: [ 'productExpiresOnIsRequired' ] },
        },
        {
        key: 'freeShipping',
        type: this.resolveFieldType('checkbox', true),
        className: '',
        hideExpression: this.fieldsHideExpressions?.freeShipping ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.freeShipping ? this.fieldsDisableExpressions.freeShipping(model) : true)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.free-shipping.label:Free shipping`,
        placeholder: $localize `:@@products.product-edit.free-shipping.placeholder:Free shipping`,
        description: '',
        hidden: !true,
            typeFormatDef: { name: 'boolean' }
        },
        },
        {
        key: 'hasDiscount',
        type: this.resolveFieldType('checkbox', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.hasDiscount ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.hasDiscount ? this.fieldsDisableExpressions.hasDiscount(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.has-discount.label:Has discount`,
        placeholder: $localize `:@@products.product-edit.has-discount.placeholder:Has discount`,
        description: '',
        hidden: !true,
            typeFormatDef: { name: 'boolean' }
        },
        },
        {
        key: 'discount',
        type: this.resolveFieldType('input', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.discount ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.discount ? this.fieldsDisableExpressions.discount(model) : false)),
        },
        templateOptions: {
        label: $localize `:@@products.product-edit.discount.label:Discount`,
        placeholder: $localize `:@@products.product-edit.discount.placeholder:Discount`,
        description: '',
        hidden: !true,
            type: 'number',
min: 0,
max: 100,

            typeFormatDef: { name: 'percent', digitsInfo: '1.2-2', locale: '' }
        },
        },
    ];
  }
}
