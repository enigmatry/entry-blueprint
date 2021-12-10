import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormlyModule } from '@ngx-formly/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { EnigmatryFormModule, ENIGMATRY_FORM_CONFIG, FormConfig } from '@enigmatry/angular-building-blocks/form';
import { FormlyExpansionPanelComponent } from './formly-expansion-panel/formly-expansion-panel.component';
import { FormlyFieldsetComponent } from './formly-fieldset/formly-fieldset.component';
import { ReadonlyBooleanComponent } from './formly-readonly-boolean/formly-readonly-boolean.component';
import { ReadonlyRadioComponent } from './formly-readonly-radio/formly-readonly-radio.component';

const defaultFormConfig: FormConfig = {
  fieldTypesConfig: {
    // Example (readonlyDefaultPrefix: 'readonly-'); Used to determine all readonly types that are not explicitly mapped in readonlyFieldTypeMappings
    readonlyDefaultPrefix: '',
    // eslint-disable-next-line id-length
    readonlyFieldTypeMappings: {
      checkbox: 'readonly-boolean',
      radio: 'readonly-radio'
    }
  }
};

@NgModule({
  declarations: [
    FormlyExpansionPanelComponent,
    FormlyFieldsetComponent,
    ReadonlyRadioComponent,
    ReadonlyBooleanComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatSelectModule,
    FormlyModule.forChild({
      types: [
        { name: 'fieldset', component: FormlyFieldsetComponent },
        { name: 'expansion-panel', component: FormlyExpansionPanelComponent },
        { name: 'readonly-boolean', component: ReadonlyBooleanComponent, wrappers: ['form-field'] },
        { name: 'readonly-radio', component: ReadonlyRadioComponent, wrappers: ['form-field'] }
      ]
    }),
    EnigmatryFormModule
  ],
  providers: [{
    provide: ENIGMATRY_FORM_CONFIG,
    useValue: defaultFormConfig
  }]
})
export class FormlyExtensionsModule { }
