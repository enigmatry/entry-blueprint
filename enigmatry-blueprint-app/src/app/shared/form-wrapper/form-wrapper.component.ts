import { Component, EventEmitter, Input, Output, TemplateRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-form-wrapper',
  templateUrl: './form-wrapper.component.html',
  styleUrls: ['./form-wrapper.component.scss']
})
export class FormWrapperComponent{
  private _isReadonly: boolean;

  @Input() saveButtonText = $localize`:@@common.save:Save`;
  @Input() cancelButtonText = $localize`:@@common.cancel:Cancel`;

  @Input() set isReadonly(value: boolean) {
    this._isReadonly = value;
  }
  get isReadonly() {
    return this._isReadonly;
  }

  @Output() apply = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  @ViewChild('defaultFormButtonsTpl') defaultFormButtonsTpl: TemplateRef<any>;
}
