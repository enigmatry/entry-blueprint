import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { APP_INITIALIZER, ApplicationConfig, importProvidersFrom, LOCALE_ID, provideZoneChangeDetection } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { authenticationInterceptor } from '@app/auth/authentication.interceptor';
import { CoreModule } from '@app/core.module';
import { AppInitService, initFactory } from '@app/services/app-init.service';

import { acceptLanguageInterceptor, EntryCommonModule } from '@enigmatry/entry-components';
import { HomeModule } from '@features/home/home.module';
import { EntryComponentsModule } from '@shared/entry-components.module';
import { SharedModule } from '@shared/shared.module';
import { ApiModule } from './api/api.module';
import { AppRoutingModule } from './app-routing.module';

export const appConfig = (i18n: { localeId: 'en-US' | 'nl-NL' }): ApplicationConfig => ({
    providers: [
        provideHttpClient(
            withInterceptors([authenticationInterceptor, acceptLanguageInterceptor])
        ),
        importProvidersFrom([
            BrowserModule,
            BrowserAnimationsModule,
            CoreModule,
            ApiModule,
            SharedModule,
            HomeModule,
            EntryCommonModule.forRoot(),
            EntryComponentsModule.forRoot(),
            AppRoutingModule
        ]),
        provideZoneChangeDetection({ eventCoalescing: true }),
        {
            provide: APP_INITIALIZER,
            useFactory: initFactory,
            deps: [AppInitService],
            multi: true
        },
        { provide: LOCALE_ID, useValue: i18n.localeId }
    ]
});
