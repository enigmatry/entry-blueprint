import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, importProvidersFrom, inject, LOCALE_ID, provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { authenticationInterceptor } from '@app/auth/authentication.interceptor';
import { CoreModule } from '@app/core.module';
import { AppInitService, initFactory } from '@app/services/app-init.service';
import { acceptLanguageInterceptor, EntryCommonModule } from '@enigmatry/entry-components';
import { EntryComponentsModule } from '@shared/entry-components.module';
import { SharedModule } from '@shared/shared.module';
import { ApiModule } from './api/api.module';
import { routes } from './app-routes';

export const appConfig = (i18n: { localeId: 'en-US' | 'nl-NL' }): ApplicationConfig => ({
    providers: [
        provideHttpClient(
            withInterceptors([authenticationInterceptor, acceptLanguageInterceptor])
        ),
        importProvidersFrom([
            BrowserModule,
            CoreModule,
            ApiModule,
            SharedModule,
            EntryCommonModule.forRoot(),
            EntryComponentsModule.forRoot()
        ]),
        provideRouter(routes),
        provideZoneChangeDetection({ eventCoalescing: true }),
        provideAppInitializer(() => {
            const initializationService = inject(AppInitService);
            const initializerFn = initFactory(initializationService);
            return initializerFn();
        }),
        { provide: LOCALE_ID, useValue: i18n.localeId }
    ]
});
