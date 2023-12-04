import { DATE_PIPE_DEFAULT_OPTIONS } from '@angular/common';
import { DEFAULT_CURRENCY_CODE, Provider } from '@angular/core';
import { MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { getCurrentLanguage } from 'src/i18n/language';
import { cultures } from './culture-info';

export const provideCurrencyCode = (): Provider => {
  return { provide: DEFAULT_CURRENCY_CODE, useValue: 'EUR' };
};

export const provideDatePipeOptions = (): Provider => {
  return {
    provide: DATE_PIPE_DEFAULT_OPTIONS,
    useFactory: () => {
      return { dateFormat: cultures[getCurrentLanguage()].datePipeFormat };
    }
  };
};

export const provideMatDateLocale = (): Provider => {
  return {
    provide: MAT_DATE_LOCALE,
    useFactory: () => cultures[getCurrentLanguage()].matDateLocale
  };
};

export const provideMatDateFormats = (): Provider => {
  const matDateFormat = cultures[getCurrentLanguage()].matDateFormat;
  return {
    provide: MAT_DATE_FORMATS,
    useValue: {
      parse: {
        dateInput: matDateFormat
      },
      display: {
        dateInput: matDateFormat,
        monthYearLabel: 'LLL uuuu',
        dateA11yLabel: 'PP',
        monthYearA11yLabel: 'LLLL uuuu'
      }
    }
  };
};