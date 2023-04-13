import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material/material.module';
import { GridCellsModule } from './grid-cells/grid-cells.module';
import { DEFAULT_DATE_FORMAT, DEFAULT_TIMEZONE, EntryTableModule } from '@enigmatry/entry-table';
import { FormlyExtensionsModule } from '../formly/formly-extensions.module';
import { FormWrapperComponent } from './form-wrapper/form-wrapper.component';
import { LookupPipe } from './pipes/lookup.pipe';
import { NotNullPipe } from './pipes/not-null.pipe';


@NgModule({
  imports: [
    CommonModule,
    MaterialModule
  ],
  declarations: [
    FormWrapperComponent,
    LookupPipe,
    NotNullPipe
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    GridCellsModule,
    EntryTableModule,
    FormlyExtensionsModule,
    FormWrapperComponent,
    LookupPipe,
    NotNullPipe
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
  ]
})
export class SharedModule { }
