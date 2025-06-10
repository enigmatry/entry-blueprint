import { Component } from '@angular/core';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { FieldType } from '@ngx-formly/material/form-field';

@Component({
  standalone: false,
  templateUrl: './formly-readonly-boolean.component.html',
  styleUrls: ['./formly-readonly-boolean.component.scss']
})
export class ReadonlyBooleanComponent extends FieldType<FormlyFieldConfig> {
}
