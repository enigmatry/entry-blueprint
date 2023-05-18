import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { GridCellsModule } from './grid-cells/grid-cells.module';
import { DEFAULT_DATE_FORMAT, DEFAULT_TIMEZONE, EntryTableModule } from '@enigmatry/entry-table';
import { FormlyExtensionsModule } from '../formly/formly-extensions.module';
import { FormWrapperComponent } from './form-wrapper/form-wrapper.component';
import { EntryDialogModule } from './entry-dialog/entry-dialog.module';
import { ENTRY_DIALOG_CONFIG, EntryDialogConfig } from './entry-dialog/models/entry-dialog-config.model';


@NgModule({
  imports: [
    CommonModule,
    MaterialModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    GridCellsModule,
    EntryTableModule,
    FormlyExtensionsModule,
    FormWrapperComponent,
    EntryDialogModule
  ],
  providers: [
    {
      provide: DEFAULT_DATE_FORMAT,
      useFactory: () => $localize`:@@common.date-format:dd. MMM yyyy. 'at' hh:mm a`
    },
    {
      provide: DEFAULT_TIMEZONE,
      useFactory: () => 'GMT'
    },
    {
      provide: ENTRY_DIALOG_CONFIG,
      useFactory: () => new EntryDialogConfig('Yes', 'No', 'align-center')
    }
  ],
  declarations: [
    FormWrapperComponent
  ]
})
export class SharedModule { }
