import { EventEmitter, TemplateRef } from '@angular/core';
import { FormGroup } from '@angular/forms';

export interface GeneratedFormComponent<T> {
  model: T;
  form: FormGroup;
  isReadonly: boolean;
  save: EventEmitter<T>;
  cancel: EventEmitter<void>;

  saveButtonText: string;
  cancelButtonText: string;
  formButtonsTemplate: TemplateRef<any> | null | undefined;
}
