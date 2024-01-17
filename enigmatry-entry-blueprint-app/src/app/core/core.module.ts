import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { AcceptLanguageInterceptor } from '@enigmatry/entry-components';
import { AuthModule } from './auth/auth.module';
import { GlobalErrorHandler } from './global-error-handler';
import { provideCurrencyCode, provideDatePipeOptions } from './i18n/localization';

@NgModule({
  imports: [
    AuthModule.forRoot()
  ],
  providers: [
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AcceptLanguageInterceptor,
      multi: true
    },
    provideCurrencyCode(),
    provideDatePipeOptions()
  ]
})
export class CoreModule { }
