import { NgModule } from '@angular/core';
import { API_BASE_URL } from '@api';
import { environment } from '@env';

@NgModule({
  declarations: [],
  imports: [],
  providers: [{
    provide: API_BASE_URL,
    useValue: environment.apiUrl
  }]
})
export class ApiModule { }
