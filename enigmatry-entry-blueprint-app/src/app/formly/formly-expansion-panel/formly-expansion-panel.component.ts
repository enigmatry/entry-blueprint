import { Component } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { FieldType, FormlyFieldConfig, FormlyModule } from '@ngx-formly/core';

@Component({
    imports: [FormlyModule, MatExpansionModule],
  templateUrl: './formly-expansion-panel.component.html',
  styleUrls: ['./formly-expansion-panel.component.scss']
})
export class FormlyExpansionPanelComponent extends FieldType<FormlyFieldConfig> {
  panelOpenState = false;
}
