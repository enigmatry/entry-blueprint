import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { GridCellsModule } from './grid-cells/grid-cells.module';
import { EnigmatryGridModule, DEFAULT_DATE_FORMAT, DEFAULT_TIMEZONE } from '@enigmatry/angular-building-blocks';
import { FormlyExtensionsModule } from '../formly/formly-extensions.module';
import { FormWrapperComponent } from './form-wrapper/form-wrapper.component';

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
    EnigmatryGridModule,
    FormlyExtensionsModule,
    FormWrapperComponent
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
