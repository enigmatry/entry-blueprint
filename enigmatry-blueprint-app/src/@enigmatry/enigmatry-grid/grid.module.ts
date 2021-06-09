import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';

import { EnigmatryGridComponent } from './grid.component';
import { EnigmatryGridCellComponent } from './cell.component';
import { EnigmatryPipesModule } from '../pipes';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatIconModule,
    EnigmatryPipesModule
  ],
  declarations: [
    EnigmatryGridComponent,
    EnigmatryGridCellComponent
  ],
  exports: [
    EnigmatryGridComponent
  ]
})
export class EnigmatryGridModule { }
