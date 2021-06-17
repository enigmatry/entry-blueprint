import { Component, DEFAULT_CURRENCY_CODE, Inject, Input, ViewEncapsulation } from '@angular/core';

import { ColumnDef } from './grid.interface';
import { DEFAULT_DATE_FORMAT } from './grid.module';

@Component({
  selector: 'enigmatry-grid-cell',
  templateUrl: './cell.component.html',
  styleUrls: ['./cell.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EnigmatryGridCellComponent {

  @Input() rowData = {};
  @Input() colDef: ColumnDef;

  constructor(
    @Inject(DEFAULT_DATE_FORMAT) public defaultDateFormat: string,
    @Inject(DEFAULT_CURRENCY_CODE) public defaultCurrencyCode: string) { }

  get colValue() {
    return this.getCellValue(this.rowData, this.colDef);
  }

  getCellValue(rowData: any, colDef: ColumnDef) {
    const keyArr = colDef.field ? colDef.field.split('.') : [];
    return keyArr.reduce((obj, key) => obj && obj[key], rowData);
  }

}
