/* eslint-disable */
import { AfterViewInit, Component, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { FieldWrapper } from '@ngx-formly/core';

@Component({
  selector: 'app-tooltip-wrapper',
  template: `
    <ng-container #fieldComponent></ng-container>
    <ng-template #tooltip>
    <mat-icon *ngIf="to.tooltipText" matTooltip="{{to.tooltipText}}">info</mat-icon>
    </ng-template>
  `
})
export class TooltipWrapperComponent extends FieldWrapper implements AfterViewInit {
  @ViewChild('fieldComponent', { read: ViewContainerRef }) fieldComponent: ViewContainerRef;
  @ViewChild('tooltip') tooltip: TemplateRef<any>;

  ngAfterViewInit() {
    if (this.tooltip) {
      setTimeout(() => this.to.suffix = this.tooltip);
    }
  }
}
