import { NgModule } from '@angular/core';
import { environment } from '@env';
import { API_BASE_URL } from './api-reference';

@NgModule({
  declarations: [],
  imports: [],
  providers: [{
    provide: API_BASE_URL,
    useValue: environment.apiUrl
  }]
})
export class ApiModule { }
