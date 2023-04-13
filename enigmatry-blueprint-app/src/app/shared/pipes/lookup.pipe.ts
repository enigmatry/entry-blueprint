import { Pipe, PipeTransform } from '@angular/core';
import { Observable } from 'rxjs';
import { LookupResponse } from 'src/app/api/api-reference';
import { LookupService } from '../services/lookup.service';

@Pipe({
  name: 'lookup'
})
export class LookupPipe implements PipeTransform {
    constructor(private lookupService: LookupService) { }

    transform = (entityType: 'product' | 'user')
        : Observable<LookupResponse[]> =>
        this.lookupService.lookup(entityType);
}
