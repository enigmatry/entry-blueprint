import { Component } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { FieldType } from '@ngx-formly/material/form-field';

@Component({
  templateUrl: './formly-readonly-boolean.component.html',
  styleUrls: ['./formly-readonly-boolean.component.scss']
})
export class ReadonlyBooleanComponent extends FieldType {
  formControl: UntypedFormControl;
}
