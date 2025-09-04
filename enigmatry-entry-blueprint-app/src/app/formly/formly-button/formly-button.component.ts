import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { FieldType } from '@ngx-formly/material/form-field';

@Component({
  imports: [MatButtonModule],
  templateUrl: './formly-button.component.html',
  styleUrls: ['./formly-button.component.scss']
})
export class FormlyButtonComponent extends FieldType<FormlyFieldConfig> {
  onClick($event: any) {
    if (this.props['onClick']) {
      this.props['onClick']($event);
    }
  }
}
