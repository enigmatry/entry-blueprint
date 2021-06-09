import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material/material.module';
import { EnigmatryPipesModule } from 'src/@enigmatry/pipes';
import { EnigmatryGridModule } from 'src/@enigmatry/enigmatry-grid';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    ReactiveFormsModule,
    MaterialModule,
    EnigmatryPipesModule,
    EnigmatryGridModule
  ]
})
export class SharedModule { }
