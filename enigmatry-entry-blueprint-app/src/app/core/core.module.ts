import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { TitleStrategy } from '@angular/router';
import { AuthModule } from '@app/auth/auth.module';
import { AcceptLanguageInterceptor } from '@enigmatry/entry-components';
import { GlobalErrorHandler } from '@services/global-error-handler';
import { PageTitleStrategy } from '@services/page-title-strategy';
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
      provide: TitleStrategy,
      useClass: PageTitleStrategy
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
