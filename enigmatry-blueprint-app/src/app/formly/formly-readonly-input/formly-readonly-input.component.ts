import { Component } from '@angular/core';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { FieldType } from '@ngx-formly/material/form-field';

@Component({
  selector: 'app-formly-readonly-input',
  templateUrl: './formly-readonly-input.component.html',
  styleUrls: ['./formly-readonly-input.component.scss']
})
export class ReadonlyInputComponent extends FieldType<FormlyFieldConfig> {
}
