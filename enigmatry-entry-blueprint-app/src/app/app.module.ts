import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AcceptLanguageInterceptor, EntryCommonModule } from '@enigmatry/entry-components/common';
import { ApiModule } from './api/api.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './core/auth/auth.module';
import { provideCurrencyCode, provideDatePipeOptions } from './core/i18n/localization';
import { AppInitService, initFactory } from './core/services/app-init.service';
import { HomeModule } from './features/home/home.module';
import { EntryComponentsModule } from './shared/entry-components.module';
import { SharedModule } from './shared/shared.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ApiModule,
    AppRoutingModule,
    SharedModule,
    HomeModule,
    AuthModule.forRoot(),
    EntryCommonModule.forRoot(),
    EntryComponentsModule.forRoot()
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initFactory,
      deps: [AppInitService],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AcceptLanguageInterceptor,
      multi: true
    },
    provideCurrencyCode(),
    provideDatePipeOptions()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
