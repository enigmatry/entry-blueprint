import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material/material.module';
import { CheckMarkPipe } from './pipes/check-mark.pipe';

@NgModule({
  declarations: [
    CheckMarkPipe
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ReactiveFormsModule,
    MaterialModule,
    CheckMarkPipe
  ]
})
export class SharedModule { }
