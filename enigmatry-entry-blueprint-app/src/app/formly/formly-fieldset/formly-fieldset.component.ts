import { Component } from '@angular/core';
import { FieldType, FormlyFieldConfig, FormlyModule } from '@ngx-formly/core';

@Component({
  imports: [FormlyModule],
  templateUrl: './formly-fieldset.component.html',
  styleUrls: ['./formly-fieldset.component.scss']
})
export class FormlyFieldsetComponent extends FieldType<FormlyFieldConfig> {
}
