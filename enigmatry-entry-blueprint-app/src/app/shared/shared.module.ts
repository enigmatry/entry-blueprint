import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { MatDateFnsModule } from '@angular/material-date-fns-adapter';
import { provideMatDateFormats, provideMatDateLocale } from '@app/i18n/localization';
import { FormlyExtensionsModule } from '../formly/formly-extensions.module';
import { EntryComponentsModule } from './entry-components.module';

@NgModule({
  imports: [
    CommonModule,
    EntryComponentsModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    MatDateFnsModule,
    FormlyExtensionsModule,
    EntryComponentsModule
  ],
  providers: [
    provideMatDateLocale(),
    provideMatDateFormats(),
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'outline', floatLabel: 'auto' } }
  ]
})
export class SharedModule { }
