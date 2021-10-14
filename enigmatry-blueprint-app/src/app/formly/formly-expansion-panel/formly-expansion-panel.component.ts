/* eslint-disable id-length */
import { Component } from '@angular/core';
import { FieldType } from '@ngx-formly/core';

@Component({
  templateUrl: './formly-expansion-panel.component.html',
  styleUrls: ['./formly-expansion-panel.component.scss']
})
export class FormlyExpansionPanelComponent extends FieldType {
  panelOpenState = false;
}
