import { Component } from '@angular/core';
import { FieldType, FormlyFieldConfig } from '@ngx-formly/core';

@Component({
  standalone: false,
  templateUrl: './formly-expansion-panel.component.html',
  styleUrls: ['./formly-expansion-panel.component.scss']
})
export class FormlyExpansionPanelComponent extends FieldType<FormlyFieldConfig> {
  panelOpenState = false;
}
