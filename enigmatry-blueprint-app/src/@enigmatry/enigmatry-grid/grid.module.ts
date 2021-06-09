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

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatIconModule
  ],
  exports: [
    EnigmatryGridComponent,
    EnigmatryGridCellComponent
  ],
  declarations: [
    EnigmatryGridComponent,
    EnigmatryGridCellComponent
  ]
})
export class EnigmatryGridModule {}
