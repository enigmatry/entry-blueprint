import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormlyExtensionsModule } from '../formly/formly-extensions.module';
import { EntryComponentsModule } from './entry-components.module';
import { FormWrapperComponent } from './form-wrapper/form-wrapper.component';
import { MaterialModule } from './material.module';
import { PipesModule } from './pipes/pipes.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    EntryComponentsModule,
    FormWrapperComponent
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FormlyExtensionsModule,
    EntryComponentsModule,
    FormWrapperComponent,
    PipesModule
  ]
})
export class SharedModule { }
