import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormlyExtensionsModule } from '../formly/formly-extensions.module';
import { EntryComponentsModule } from './entry-components.module';
import { FormWrapperComponent } from './form-wrapper/form-wrapper.component';
import { GridCellsModule } from './grid-cells/grid-cells.module';
import { MaterialModule } from './material.module';
import { PipesModule } from './pipes/pipes.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    EntryComponentsModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    GridCellsModule,
    FormlyExtensionsModule,
    EntryComponentsModule,
    FormWrapperComponent,
    PipesModule
  ],
  declarations: [
    FormWrapperComponent
  ]
})
export class SharedModule { }
