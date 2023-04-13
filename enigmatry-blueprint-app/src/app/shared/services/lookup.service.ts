import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { LookupResponse, UsersClient } from 'src/app/api/api-reference';

@Injectable({
  providedIn: 'root'
})
export class LookupService {
  private readonly _defaultLookupPageSize = 1000;
  private readonly _defaultLookupSortField = 'displayName';

  constructor(private usersClient: UsersClient) { }

  lookup = (entityType: 'product' | 'user')
    : Observable<LookupResponse[]> => {
    switch (entityType) {
      case 'user':
        return this.usersClient
          .lookup(
            undefined,
            this._defaultLookupPageSize,
            this._defaultLookupSortField,
            undefined)
          .pipe(map(x => x.items ?? []));
      default: {
        return of([]);
      }
    }
  };
}
