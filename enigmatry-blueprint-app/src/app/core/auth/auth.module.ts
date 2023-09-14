import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { AuthInterceptor } from './auth.interceptor';
import { AuthService } from './auth.service';
import { msalConfigFactory, MSAL_CONFIG } from './msal-config';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class AuthModule {
  static forRoot(): ModuleWithProviders<AuthModule> {
    return {
      ngModule: AuthModule,
      providers: [
        {
          provide: MSAL_CONFIG,
          useFactory: msalConfigFactory
        },
        {
          provide: HTTP_INTERCEPTORS,
          useClass: AuthInterceptor,
          multi: true
        },
        AuthService
      ]
    };
  }
}
