import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormlyExpansionPanelComponent } from './formly-expansion-panel/formly-expansion-panel.component';
import { FormlyFieldsetComponent } from './formly-fieldset/formly-fieldset.component';
import { FormlyModule } from '@ngx-formly/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    FormlyExpansionPanelComponent,
    FormlyFieldsetComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatSelectModule,
    FormlyModule.forChild({
      types: [
        { name: 'fieldset', component: FormlyFieldsetComponent },
        { name: 'expansion-panel', component: FormlyExpansionPanelComponent }
      ]
    })
  ]
})
export class FormlyExtensionsModule { }
