import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { AuthService } from './auth.service';
import { MSAL_CONFIG, msalConfigFactory } from './msal-config';

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
        AuthService
      ]
    };
  }
}
