import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { FieldType } from '@ngx-formly/material/form-field';
import { BehaviorSubject, isObservable } from 'rxjs';
import { map } from 'rxjs/operators';

declare type SelectOption = {
  value: any;
  displayName: string;
};

@Component({
  templateUrl: './formly-readonly-radio.component.html',
  styleUrls: ['./formly-readonly-radio.component.scss']
})
export class ReadonlyRadioComponent extends FieldType {
    formControl: FormControl;
    optionLabel = new BehaviorSubject<string>('');
    option$ = this.optionLabel.asObservable();

    isArray = (): boolean => Array.isArray(this.to.options);

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
      if (isObservable(this.to.options)) {
        this.to.options
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
      if (Array.isArray(this.to.options) && this.to.options.length > 0) {
        return this.to.options.map(opt => opt as SelectOption);
      }
      return undefined;
    };
  }
