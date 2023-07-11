import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EntryValidationModule } from '@enigmatry/entry-components/validation';
import { DEFAULT_DATE_FORMAT, DEFAULT_TIMEZONE, EntryTableModule } from '@enigmatry/entry-table';
import { FormlyExtensionsModule } from '../formly/formly-extensions.module';
import { FormWrapperComponent } from './form-wrapper/form-wrapper.component';
import { GridCellsModule } from './grid-cells/grid-cells.module';
import { MaterialModule } from './material.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    EntryValidationModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    GridCellsModule,
    EntryTableModule,
    FormlyExtensionsModule,
    FormWrapperComponent,
    EntryValidationModule
  ],
  providers: [
    {
      provide: DEFAULT_DATE_FORMAT,
      useFactory: () => $localize`:@@common.date-format:dd. MMM yyyy. 'at' hh:mm a`
    },
    {
      provide: DEFAULT_TIMEZONE,
      useFactory: () => 'GMT'
    }
  ],
  declarations: [
    FormWrapperComponent
  ]
})
export class SharedModule { }
