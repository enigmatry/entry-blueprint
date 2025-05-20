import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  standalone: false,
  name: 'notNull'
})
export class NotNullPipe implements PipeTransform {
  transform = (value: any[] | null): any[] => value ?? [];
}
