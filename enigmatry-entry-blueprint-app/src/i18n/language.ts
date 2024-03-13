import { environment } from '@env';

export const localizations = {
  en: () => import('./en'),
  nl: () => import('./nl')
};

export type Language = keyof typeof localizations;

export const getSupportedLanguages = (): Language[] => Object.keys(localizations) as Language[];

// Saved -> Browser -> Environment !?
export const getCurrentLanguage = (): Language => {
  // Saved language
  const lang = localStorage.getItem('lang') ?? '';
  if (lang in localizations) {
    return lang as Language;
  }

  // Fallback to browser language
  const browserLanguage = window.navigator.language.split('-')[0];
  if (browserLanguage in localizations) {
    return browserLanguage as Language;
  }

  // Fallback to environment default
  if (environment.defaultLanguage in localizations) {
    return environment.defaultLanguage as Language;
  }

  // Fallback to any language
  // eslint-disable-next-line no-console
  console.log('Default language is not set. Falling back to EN.');
  return 'en';
};

export const setCurrentLanguage = (lang: Language) => {
  localStorage.setItem('lang', lang);
};
