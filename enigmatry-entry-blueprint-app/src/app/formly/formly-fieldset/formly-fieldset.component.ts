import { Component } from '@angular/core';
import { FieldType, FormlyFieldConfig } from '@ngx-formly/core';

@Component({
  standalone: false,
  templateUrl: './formly-fieldset.component.html',
  styleUrls: ['./formly-fieldset.component.scss']
})
export class FormlyFieldsetComponent extends FieldType<FormlyFieldConfig> {
}
