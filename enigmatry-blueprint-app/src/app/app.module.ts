import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, DEFAULT_CURRENCY_CODE, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ApiModule } from './api/api.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './core/auth/auth.module';
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
    EntryComponentsModule.forRoot()
  ],
  providers: [
    { provide: DEFAULT_CURRENCY_CODE, useValue: 'EUR' },
    {
      provide: APP_INITIALIZER,
      useFactory: initFactory,
      deps: [AppInitService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
