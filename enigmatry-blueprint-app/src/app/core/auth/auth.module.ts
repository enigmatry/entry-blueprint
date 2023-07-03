import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { msalConfigFactory, MSAL_CONFIG } from './azure-ad-b2c/msal-config';
import { MsalAuthService } from './azure-ad-b2c/msal-auth.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';

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
        MsalAuthService
      ]
    };
  }
}
