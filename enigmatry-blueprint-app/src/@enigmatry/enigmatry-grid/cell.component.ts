import { Component, Input, ViewEncapsulation } from '@angular/core';

import { ColumnDef  } from './grid.interface';

@Component({
  selector: 'enigmatry-grid-cell',
  templateUrl: './cell.component.html',
  styleUrls: ['./cell.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EnigmatryGridCellComponent {
  @Input() rowData = {};
  @Input() colDef: ColumnDef;

  get colValue() {
    return this.getCellValue(this.rowData, this.colDef) || '';
  }

  getCellValue(rowData: any, colDef: ColumnDef): string {
    const keyArr = colDef.field ? colDef.field.split('.') : [];
    return keyArr.reduce((obj, key) => obj && obj[key], rowData);
  }

}
