// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------;
/* eslint-disable */
import { Component, EventEmitter, Inject, Input, LOCALE_ID, OnInit, OnDestroy, Optional, Output, TemplateRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { IGetUserDetailsResponse } from 'src/app/api/api-reference';
import { IFieldExpressionDictionary, IFieldPropertyExpressionDictionary, SelectConfiguration, ENTRY_FIELD_TYPE_RESOLVER, FieldTypeResolver, sortOptions } from '@enigmatry/entry-form';
import { BehaviorSubject, of, Subject, Subscription } from 'rxjs';
import { map, throttleTime } from 'rxjs/operators';


@Component({
  selector: 'app-g-user-edit',
  templateUrl: './user-edit-generated.component.html'
})
export class UserEditGeneratedComponent implements OnInit, OnDestroy {

  @Input() model: IGetUserDetailsResponse = {} as IGetUserDetailsResponse;
  @Input() set isReadonly(value: boolean) {
    this._isReadonly = value;
    this.fields = this.initializeFields();
  }
  get isReadonly() {
    return this._isReadonly;
  }

  @Input() saveButtonText: string = 'Save';
  @Input() cancelButtonText: string = 'Cancel';
  @Input() saveButtonDisabled: boolean = false;
  @Input() formButtonsTemplate: TemplateRef<any> | null | undefined;

  @Input() fieldsHideExpressions: IFieldExpressionDictionary<IGetUserDetailsResponse> | undefined = undefined;
  @Input() fieldsDisableExpressions: IFieldExpressionDictionary<IGetUserDetailsResponse> | undefined = undefined;
  @Input() fieldsRequiredExpressions: IFieldExpressionDictionary<IGetUserDetailsResponse> | undefined = undefined;
  @Input() fieldsPropertyExpressions: IFieldPropertyExpressionDictionary<IGetUserDetailsResponse> | undefined = undefined;
  @Input() fieldsLabelExpressions: IFieldPropertyExpressionDictionary<IGetUserDetailsResponse> | undefined = undefined;

  @Output() save = new EventEmitter<IGetUserDetailsResponse>();
  @Output() cancel = new EventEmitter<void>();
  @Output() buttonClick = new EventEmitter<string>();

            private roleIdOptions$ = new BehaviorSubject<any[]>([]);
            @Input()
            get roleIdOptions(): any[] { return this.roleIdOptions$.value; }
            set roleIdOptions(value: any[]) { this.roleIdOptions$.next(value); }
            @Input() roleIdOptionsConfiguration: SelectConfiguration = {};

  _isReadonly: boolean;
  form = new FormGroup({});
  fields: FormlyFieldConfig[] = [];
  private _submitClicks = new Subject<void>();
  private _submitClicksSubscription: Subscription;

  constructor(
    @Inject(LOCALE_ID) private _localeId: string,
    @Optional() @Inject(ENTRY_FIELD_TYPE_RESOLVER) private _fieldTypeResolver: FieldTypeResolver) { }

  ngOnInit(): void {
    this.fields = this.initializeFields();
    this._submitClicksSubscription = this._submitClicks
        .pipe(throttleTime(500))
        .subscribe(() => this.save.emit(this.model));
  }

  ngOnDestroy(): void {
      this._submitClicksSubscription.unsubscribe();
  }

  onSubmit() {
    if (this.form.valid) {
      this._submitClicks.next();
    }
  }

  resolveFieldType = (type: string, isControlReadonly: boolean): string =>
    this._fieldTypeResolver ? this._fieldTypeResolver(type, this.isReadonly || isControlReadonly) : type;

  initializeFields(): FormlyFieldConfig[] {
    return [
        {
        type: this.resolveFieldType('', false),
fieldGroupClassName: `entry-field-group`,
        templateOptions: {
        label: $localize `:@@users.user-edit.user.label:User`,
        placeholder: $localize `:@@users.user-edit.user.placeholder:User`,
        disabled: this.isReadonly || false,
        description: ``
        },
        fieldGroup:[
        {
        key: 'emailAddress',
        type: this.resolveFieldType('input', true),
        focus: false,
className: `entry-email-address-field entry-input`,
        hideExpression: this.fieldsHideExpressions?.emailAddress ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.emailAddress ? this.fieldsDisableExpressions.emailAddress(model) : true)),
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.emailAddress ? this.fieldsRequiredExpressions.emailAddress(model) : false),
        'templateOptions.label': (model) => (this.fieldsLabelExpressions?.emailAddress ? this.fieldsLabelExpressions.emailAddress(model) : $localize `:@@users.user-edit.email-address.label:Email address`),
        'model.emailAddress': (model) => (this.fieldsPropertyExpressions?.emailAddress ? this.fieldsPropertyExpressions.emailAddress(model) : model.emailAddress),
        },
        templateOptions: {
        label: $localize `:@@users.user-edit.email-address.label:Email address`,
        placeholder: $localize `:@@users.user-edit.email-address.placeholder:Email address`,
        description: ``,
            attributes: {  },
            hidden: !true,
            typeFormatDef: undefined
        },
        },
        {
        key: 'fullName',
        type: this.resolveFieldType('input', false),
        focus: false,
className: `entry-full-name-field entry-input`,
        hideExpression: this.fieldsHideExpressions?.fullName ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.fullName ? this.fieldsDisableExpressions.fullName(model) : false)),
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.fullName ? this.fieldsRequiredExpressions.fullName(model) : true),
        'templateOptions.label': (model) => (this.fieldsLabelExpressions?.fullName ? this.fieldsLabelExpressions.fullName(model) : $localize `:@@users.user-edit.full-name.label:Full name`),
        'model.fullName': (model) => (this.fieldsPropertyExpressions?.fullName ? this.fieldsPropertyExpressions.fullName(model) : model.fullName),
        },
        templateOptions: {
        label: $localize `:@@users.user-edit.full-name.label:Full name`,
        placeholder: $localize `:@@users.user-edit.full-name.placeholder:Full name`,
        description: ``,
            attributes: {  },
            hidden: !true,
            required: true,
maxLength: 200,

            typeFormatDef: undefined
        },
        },
        {
        key: 'roleId',
        type: this.resolveFieldType('select', false),
        focus: false,
className: `entry-role-id-field entry-select`,
        hideExpression: this.fieldsHideExpressions?.roleId ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.roleId ? this.fieldsDisableExpressions.roleId(model) : false)),
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.roleId ? this.fieldsRequiredExpressions.roleId(model) : false),
        'templateOptions.label': (model) => (this.fieldsLabelExpressions?.roleId ? this.fieldsLabelExpressions.roleId(model) : $localize `:@@users.user-edit.role-id.label:Role`),
        'model.roleId': (model) => (this.fieldsPropertyExpressions?.roleId ? this.fieldsPropertyExpressions.roleId(model) : model.roleId),
        },
        templateOptions: {
        label: $localize `:@@users.user-edit.role-id.label:Role`,
        placeholder: $localize `:@@users.user-edit.role-id.placeholder:Role`,
        description: ``,
            options: this.roleIdOptions$.pipe(map(opts => sortOptions(opts, this.roleIdOptionsConfiguration.valueProperty, this.roleIdOptionsConfiguration.sortProperty, this._localeId))),
            valueProp: this.roleIdOptionsConfiguration.valueProperty,
            labelProp: this.roleIdOptionsConfiguration.labelProperty,
            attributes: {  },
            hidden: !true,
            typeFormatDef: undefined
        },
        },
        {
        type: this.resolveFieldType('', false),
fieldGroupClassName: `entry-field-group`,
        templateOptions: {
        label: $localize `:@@users.user-edit.history.label:History`,
        placeholder: $localize `:@@users.user-edit.history.placeholder:History`,
        disabled: this.isReadonly || false,
        description: ``
        },
        fieldGroup:[
        {
        key: 'createdOn',
        type: this.resolveFieldType('datepicker', true),
        focus: false,
className: `entry-created-on-field entry-datepicker`,
        hideExpression: this.fieldsHideExpressions?.createdOn ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.createdOn ? this.fieldsDisableExpressions.createdOn(model) : true)),
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.createdOn ? this.fieldsRequiredExpressions.createdOn(model) : false),
        'templateOptions.label': (model) => (this.fieldsLabelExpressions?.createdOn ? this.fieldsLabelExpressions.createdOn(model) : $localize `:@@users.user-edit.created-on.label:Created on`),
        'model.createdOn': (model) => (this.fieldsPropertyExpressions?.createdOn ? this.fieldsPropertyExpressions.createdOn(model) : model.createdOn),
        },
        templateOptions: {
        label: $localize `:@@users.user-edit.created-on.label:Created on`,
        placeholder: $localize `:@@users.user-edit.created-on.placeholder:Created on`,
        description: ``,
            attributes: {  },
            hidden: !true,
            typeFormatDef: { name: 'date' }
        },
modelOptions: { updateOn: 'blur' },
        },
        {
        key: 'updatedOn',
        type: this.resolveFieldType('datepicker', true),
        focus: false,
className: `entry-updated-on-field entry-datepicker`,
        hideExpression: this.fieldsHideExpressions?.updatedOn ?? false,
        expressionProperties: {
        'templateOptions.disabled': (model) => (this.isReadonly || (this.fieldsDisableExpressions?.updatedOn ? this.fieldsDisableExpressions.updatedOn(model) : true)),
        'templateOptions.required': (model) => (this.fieldsRequiredExpressions?.updatedOn ? this.fieldsRequiredExpressions.updatedOn(model) : false),
        'templateOptions.label': (model) => (this.fieldsLabelExpressions?.updatedOn ? this.fieldsLabelExpressions.updatedOn(model) : $localize `:@@users.user-edit.updated-on.label:Updated on`),
        'model.updatedOn': (model) => (this.fieldsPropertyExpressions?.updatedOn ? this.fieldsPropertyExpressions.updatedOn(model) : model.updatedOn),
        },
        templateOptions: {
        label: $localize `:@@users.user-edit.updated-on.label:Updated on`,
        placeholder: $localize `:@@users.user-edit.updated-on.placeholder:Updated on`,
        description: ``,
            attributes: {  },
            hidden: !true,
            typeFormatDef: { name: 'date' }
        },
modelOptions: { updateOn: 'blur' },
        },
        ]
        },
        ]
        },
    ];
}

  private applyOptionally<T>(value: T, apply: boolean): T | undefined {
    return apply ? value : undefined;
  }

}
