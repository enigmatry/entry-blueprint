import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { GridCellsModule } from './grid-cells/grid-cells.module';
import { DEFAULT_DATE_FORMAT, DEFAULT_TIMEZONE, EntryTableModule } from '@enigmatry/entry-table';
import { FormlyExtensionsModule } from '../formly/formly-extensions.module';
import { FormWrapperComponent } from './form-wrapper/form-wrapper.component';
import { EntryValidationModule } from '@enigmatry/entry-components/validation';

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
