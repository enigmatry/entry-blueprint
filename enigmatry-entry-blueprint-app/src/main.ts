import { registerLocaleData } from '@angular/common';
import { LOCALE_ID, enableProdMode } from '@angular/core';
import { loadTranslations } from '@angular/localize';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { environment } from '@env';
import { AppModule } from './app/app.module';
import { getCurrentLanguage, localizations } from './i18n/language';

if (environment.production) {
  enableProdMode();
}

const language = getCurrentLanguage();
const getLanguageData = localizations[language];

getLanguageData()
  .then(i18n => {
    registerLocaleData(i18n.locale, i18n.localeId, i18n.localeExtra);
    loadTranslations(i18n.messages.translations);

    platformBrowserDynamic([
      { provide: LOCALE_ID, useValue: i18n.localeId }
    ]).bootstrapModule(AppModule);
  })
  // eslint-disable-next-line no-console
  .catch(err => console.error(err));
