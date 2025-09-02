import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, ErrorHandler, importProvidersFrom, inject, LOCALE_ID,
     provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideRouter, TitleStrategy } from '@angular/router';
import { API_BASE_URL } from '@api';
import { authenticationInterceptor } from '@app/auth/authentication.interceptor';
import { provideCurrencyCode, provideDatePipeOptions } from '@app/i18n/localization';
import { AppInitService, initFactory } from '@app/services/app-init.service';
import { GlobalErrorHandler } from '@app/services/global-error-handler';
import { PageTitleStrategy } from '@app/services/page-title-strategy';
import { acceptLanguageInterceptor, EntryCommonModule } from '@enigmatry/entry-components';
import { environment } from '@env';
import { EntryComponentsModule } from '@shared/entry-components.module';
import { SharedModule } from '@shared/shared.module';
import { routes } from './app-routes';

export const appConfig = (i18n: { localeId: 'en-US' | 'nl-NL' }): ApplicationConfig => ({
    providers: [
        {
            provide: API_BASE_URL,
            useValue: environment.apiUrl
        },
        {
            provide: ErrorHandler,
            useClass: GlobalErrorHandler
        },
        {
            provide: TitleStrategy,
            useClass: PageTitleStrategy
        },
        provideHttpClient(
            withInterceptors([authenticationInterceptor, acceptLanguageInterceptor])
        ),
        importProvidersFrom([
            BrowserModule,
            SharedModule,
            EntryCommonModule.forRoot(),
            EntryComponentsModule.forRoot()
        ]),
        provideCurrencyCode(),
        provideDatePipeOptions(),
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
