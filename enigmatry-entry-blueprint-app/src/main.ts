import { registerLocaleData } from '@angular/common';
import { enableProdMode } from '@angular/core';
import { loadTranslations } from '@angular/localize';
import { bootstrapApplication } from '@angular/platform-browser';
import { environment } from '@env';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';
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
    bootstrapApplication(AppComponent, appConfig(i18n));
  })
  // eslint-disable-next-line no-console
  .catch(err => console.error(err));
