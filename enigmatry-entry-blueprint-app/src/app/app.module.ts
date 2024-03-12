import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from '@app/core.module';
import { AppInitService, initFactory } from '@app/services/app-init.service';
import { EntryCommonModule } from '@enigmatry/entry-components/common';
import { HomeModule } from '@features/home/home.module';
import { EntryComponentsModule } from '@shared/entry-components.module';
import { SharedModule } from '@shared/shared.module';
import { ApiModule } from './api/api.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CoreModule,
    ApiModule,
    SharedModule,
    HomeModule,
    EntryCommonModule.forRoot(),
    EntryComponentsModule.forRoot(),
    AppRoutingModule
  ],
  providers: [
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
