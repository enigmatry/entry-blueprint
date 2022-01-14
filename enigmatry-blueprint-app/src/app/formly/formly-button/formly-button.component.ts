import { Component } from '@angular/core';
import { FieldType } from '@ngx-formly/material/form-field';

@Component({
  templateUrl: './formly-button.component.html',
  styleUrls: ['./formly-button.component.scss']
})
export class FormlyButtonComponent extends FieldType {
  onClick($event: any) {
    if (this.to.onClick) {
      this.to.onClick($event);
    }
  }
}
