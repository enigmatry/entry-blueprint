import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material/material.module';
import { EnigmatryGridModule } from 'src/@enigmatry/enigmatry-grid';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    ReactiveFormsModule,
    MaterialModule,
    EnigmatryGridModule
  ]
})
export class SharedModule { }
