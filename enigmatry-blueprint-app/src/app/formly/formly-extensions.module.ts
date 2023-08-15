import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { FORM_FIELD_ERROR_KEY } from '@enigmatry/entry-components/validation';
import { EntryFormConfig, EntryFormModule, ENTRY_FORM_CONFIG } from '@enigmatry/entry-form';
import { EntryTableModule } from '@enigmatry/entry-table';
import { FormlyModule } from '@ngx-formly/core';
import { FormlyMaterialModule } from '@ngx-formly/material';
import { FormlyButtonComponent } from './formly-button/formly-button.component';
import { FormlyExpansionPanelComponent } from './formly-expansion-panel/formly-expansion-panel.component';
import { FormlyFieldsetComponent } from './formly-fieldset/formly-fieldset.component';
import { ReadonlyBooleanComponent } from './formly-readonly-boolean/formly-readonly-boolean.component';
import { ReadonlyInputComponent } from './formly-readonly-input/formly-readonly-input.component';
import { ReadonlyRadioComponent } from './formly-readonly-radio/formly-readonly-radio.component';

const defaultFormConfig: EntryFormConfig = {
  fieldTypesConfig: {
    // Example (readonlyDefaultPrefix: 'readonly-'); Used to determine all readonly types that are not explicitly mapped in readonlyFieldTypeMappings
    readonlyDefaultPrefix: '',
    readonlyFieldTypeMappings: {
      input: 'readonly-input',
      checkbox: 'readonly-boolean',
      radio: 'readonly-radio',
      datepicker: 'readonly-input'
    }
  }
};

@NgModule({
  declarations: [
    FormlyExpansionPanelComponent,
    FormlyFieldsetComponent,
    ReadonlyInputComponent,
    ReadonlyRadioComponent,
    ReadonlyBooleanComponent,
    FormlyButtonComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatSelectModule,
    MatButtonModule,
    EntryFormModule,
    EntryTableModule,
    FormlyMaterialModule,
    FormlyModule.forChild({
      types: [
        { name: 'fieldset', component: FormlyFieldsetComponent },
        { name: 'expansion-panel', component: FormlyExpansionPanelComponent },
        { name: 'readonly-input', component: ReadonlyInputComponent, wrappers: ['form-field'] },
        { name: 'readonly-boolean', component: ReadonlyBooleanComponent, wrappers: ['form-field'] },
        { name: 'readonly-radio', component: ReadonlyRadioComponent, wrappers: ['form-field'] },
        {
          name: 'button',
          component: FormlyButtonComponent,
          wrappers: [],
          defaultOptions: {
            templateOptions: {
              type: 'button',
              color: 'primary'
            }
          }
        },
        {
          name: 'checkbox',
          wrappers: []
        }
      ],
      validationMessages: [
        { name: FORM_FIELD_ERROR_KEY, message: (error, _) => error },
        { name: 'required', message: 'Required field.' }
      ]
    })
  ],
  providers: [{
    provide: ENTRY_FORM_CONFIG,
    useValue: defaultFormConfig
  }]
})
export class FormlyExtensionsModule { }
