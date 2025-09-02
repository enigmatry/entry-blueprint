import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { FieldType } from '@ngx-formly/material/form-field';
import { BehaviorSubject, isObservable } from 'rxjs';
import { map } from 'rxjs/operators';

declare interface SelectOption {
  value: any;
  displayName: string;
}

@Component({
  imports: [CommonModule],
  templateUrl: './formly-readonly-radio.component.html',
  styleUrls: ['./formly-readonly-radio.component.scss']
})
export class ReadonlyRadioComponent extends FieldType<FormlyFieldConfig> {
  optionLabel = new BehaviorSubject<string>('');
  option$ = this.optionLabel.asObservable();

  isArray = (): boolean => Array.isArray(this.props.options);

  getLabelFromArray(value: number) {
    const options = this.tryGetOptions();
    if (options) {
      const found = options.find(opt => opt.value === value);
      if (found) {
        return found.displayName;
      }
    }
    return value.toString();
  }

  getLabelFromObservable(value: number) {
    if (isObservable(this.props.options)) {
      this.props.options
        .pipe(map(options => options.map(opt => opt as SelectOption)))
        .subscribe(options => {
          const found = options.find(opt => opt.value === value);
          if (found) {
            this.optionLabel.next(found.displayName);
          }
        });
    }

    return this.option$;
  }

  private tryGetOptions = (): SelectOption[] | undefined => {
    if (Array.isArray(this.props.options) && this.props.options.length > 0) {
      return this.props.options.map(opt => opt as SelectOption);
    }
    return undefined;
  };
}
