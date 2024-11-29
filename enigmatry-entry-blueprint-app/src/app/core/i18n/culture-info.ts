import { nl, enUS, Locale } from 'date-fns/locale';
import { Language } from 'src/i18n/language';

export interface CultureInfo {
  datePipeFormat: string;
  matDateFormat: string;
  matDateLocale: Locale;
}

export const cultures: Record<Language, CultureInfo> = {
  en: {
    datePipeFormat: 'MM/dd/yyyy',
    matDateFormat: 'MM/dd/yyyy',
    matDateLocale: enUS
  },
  nl: {
    datePipeFormat: 'dd-MM-yyyy',
    matDateFormat: 'dd-MM-yyyy',
    matDateLocale: nl
  }
};