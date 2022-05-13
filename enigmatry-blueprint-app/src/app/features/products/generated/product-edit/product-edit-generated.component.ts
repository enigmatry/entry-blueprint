// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------;
/* eslint-disable */
import { Component, EventEmitter, Inject, Input, LOCALE_ID, OnInit, Optional, Output, TemplateRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { IGetProductDetailsResponse } from 'src/app/api/api-reference';
import { IFieldExpressionDictionary, IFieldPropertyExpressionDictionary, SelectConfiguration, ENIGMATRY_FIELD_TYPE_RESOLVER, FieldTypeResolver, sortOptions } from '@enigmatry/angular-building-blocks/form';
import { BehaviorSubject, of } from 'rxjs';
import { map } from 'rxjs/operators';


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
  @Input() fieldsRequiredExpressions: IFieldExpressionDictionary<IGetProductDetailsResponse> | undefined = undefined;
  @Input() fieldsPropertyExpressions: IFieldPropertyExpressionDictionary<IGetProductDetailsResponse> | undefined = undefined;

  @Output() save = new EventEmitter<IGetProductDetailsResponse>();
  @Output() cancel = new EventEmitter<void>();
  @Output() buttonClick = new EventEmitter<string>();

                @Input() typeOptions: any[] = [{ value: 0, displayName: 'Food' }, { value: 1, displayName: 'Drink' }, { value: 2, displayName: 'Book' }, { value: 3, displayName: 'Car' }];
                @Input() typeOptionsConfiguration: SelectConfiguration = { valueProperty: 'value', labelProperty: 'displayName', sortProperty: 'displayName' };

  _isReadonly: boolean;
  form = new FormGroup({});
  fields: FormlyFieldConfig[] = [];

  constructor(
    @Inject(LOCALE_ID) private _localeId: string,
    @Optional() @Inject(ENIGMATRY_FIELD_TYPE_RESOLVER) private _fieldTypeResolver: FieldTypeResolver) { }

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
        {
        key: 'name',
        type: this.resolveFieldType('input', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.name ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.name ? this.fieldsDisableExpressions.name(model) : false)),
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.name ? this.fieldsRequiredExpressions.name(model) : true),
        'model.name': (model) => (this.fieldsPropertyExpressions?.name ? this.fieldsPropertyExpressions.name(model) : model.name),
        },
        templateOptions: {
        label: 'Name',
        placeholder: 'Unique product name',
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.code ? this.fieldsRequiredExpressions.code(model) : true),
        'model.code': (model) => (this.fieldsPropertyExpressions?.code ? this.fieldsPropertyExpressions.code(model) : model.code),
        },
        templateOptions: {
        label: 'Code',
        placeholder: 'Unique product code identifier',
        description: '',
        hidden: !true,
            required: true,
pattern: /^[A-Z]{4}[1-9]{8}$/mu,

            typeFormatDef: undefined
        },
            validation: {
            messages: {
pattern: 'Code must be in 4 letter 8 digits format (e.g. ABCD12345678)'
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.type ? this.fieldsRequiredExpressions.type(model) : true),
        'model.type': (model) => (this.fieldsPropertyExpressions?.type ? this.fieldsPropertyExpressions.type(model) : model.type),
        },
        templateOptions: {
        label: 'Type',
        placeholder: 'Type',
        description: '',
            options: of(this.typeOptions).pipe(map(opts => sortOptions(opts, this.typeOptionsConfiguration.valueProperty, this.typeOptionsConfiguration.sortProperty, this._localeId))),
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.description ? this.fieldsRequiredExpressions.description(model) : false),
        'model.description': (model) => (this.fieldsPropertyExpressions?.description ? this.fieldsPropertyExpressions.description(model) : model.description),
        },
        templateOptions: {
        label: 'Description',
        placeholder: 'Description',
        description: '',
            rows: 2,
            cols: 0,
            autosize: false,
            autosizeMinRows: 0,
            autosizeMaxRows: 0,
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.price ? this.fieldsRequiredExpressions.price(model) : true),
        'model.price': (model) => (this.fieldsPropertyExpressions?.price ? this.fieldsPropertyExpressions.price(model) : model.price),
        },
        templateOptions: {
        label: 'Price per unit',
        placeholder: 'Price per unit',
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.amount ? this.fieldsRequiredExpressions.amount(model) : true),
        'model.amount': (model) => (this.fieldsPropertyExpressions?.amount ? this.fieldsPropertyExpressions.amount(model) : model.amount),
        },
        templateOptions: {
        label: 'Units',
        placeholder: 'Units',
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.contactEmail ? this.fieldsRequiredExpressions.contactEmail(model) : true),
        'model.contactEmail': (model) => (this.fieldsPropertyExpressions?.contactEmail ? this.fieldsPropertyExpressions.contactEmail(model) : model.contactEmail),
        },
        templateOptions: {
        label: 'Contact email',
        placeholder: 'Contact person email address',
        description: '',
        hidden: !true,
            required: true,
pattern: /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/,

            typeFormatDef: undefined
        },
            validation: {
            messages: {
pattern: 'Invalid email address format'
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.contactPhone ? this.fieldsRequiredExpressions.contactPhone(model) : true),
        'model.contactPhone': (model) => (this.fieldsPropertyExpressions?.contactPhone ? this.fieldsPropertyExpressions.contactPhone(model) : model.contactPhone),
        },
        templateOptions: {
        label: 'Contact phone',
        placeholder: 'Contact person phone number',
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.infoLink ? this.fieldsRequiredExpressions.infoLink(model) : false),
        'model.infoLink': (model) => (this.fieldsPropertyExpressions?.infoLink ? this.fieldsPropertyExpressions.infoLink(model) : model.infoLink),
        },
        templateOptions: {
        label: 'Homepage',
        placeholder: 'Link to product homepage',
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.expiresOn ? this.fieldsRequiredExpressions.expiresOn(model) : false),
        'model.expiresOn': (model) => (this.fieldsPropertyExpressions?.expiresOn ? this.fieldsPropertyExpressions.expiresOn(model) : model.expiresOn),
        },
        templateOptions: {
        label: 'Expires on',
        placeholder: 'Product expiration date, if any',
        description: '',
        hidden: !true,
            typeFormatDef: { name: 'date' }
        },
        },
        {
        key: 'freeShipping',
        type: this.resolveFieldType('checkbox', true),
        className: '',
        hideExpression: this.fieldsHideExpressions?.freeShipping ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.freeShipping ? this.fieldsDisableExpressions.freeShipping(model) : true)),
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.freeShipping ? this.fieldsRequiredExpressions.freeShipping(model) : false),
        'model.freeShipping': (model) => (this.fieldsPropertyExpressions?.freeShipping ? this.fieldsPropertyExpressions.freeShipping(model) : model.freeShipping),
        },
        templateOptions: {
        label: 'Free shipping',
        placeholder: 'Free shipping',
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.hasDiscount ? this.fieldsRequiredExpressions.hasDiscount(model) : false),
        'model.hasDiscount': (model) => (this.fieldsPropertyExpressions?.hasDiscount ? this.fieldsPropertyExpressions.hasDiscount(model) : model.hasDiscount),
        },
        templateOptions: {
        label: 'Has discount',
        placeholder: 'Has discount',
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
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.discount ? this.fieldsRequiredExpressions.discount(model) : false),
        'model.discount': (model) => (this.fieldsPropertyExpressions?.discount ? this.fieldsPropertyExpressions.discount(model) : model.discount),
        },
        templateOptions: {
        label: 'Discount',
        placeholder: 'Discount',
        description: '',
        hidden: !true,
            type: 'number',
min: 0,
max: 100,

            typeFormatDef: { name: 'percent', digitsInfo: '1.2-2', locale: '' }
        },
        },
        {
        key: 'resetFormBtn',
        type: this.resolveFieldType('button', false),
        className: '',
        hideExpression: this.fieldsHideExpressions?.resetFormBtn ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.resetFormBtn ? this.fieldsDisableExpressions.resetFormBtn(model) : false)),
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.resetFormBtn ? this.fieldsRequiredExpressions.resetFormBtn(model) : false),
        'model.resetFormBtn': (model) => (this.fieldsPropertyExpressions?.resetFormBtn ? this.fieldsPropertyExpressions.resetFormBtn(model) : model.resetFormBtn),
        },
        templateOptions: {
        label: '',
        placeholder: '',
        description: '* This will reset form to its initial state.',
            text: 'Reset',
            onClick: ($event: any) => this.buttonClick.emit('resetFormBtn'),
        hidden: !true,
        },
        },
            { key: 'id' },
    ];
  }
}
