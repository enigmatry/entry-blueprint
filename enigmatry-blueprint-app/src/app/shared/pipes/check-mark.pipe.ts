import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'checkMark'
})
export class CheckMarkPipe implements PipeTransform {

  transform(value: boolean): string {
    return value ? '\u2713' : '';
  }

}
