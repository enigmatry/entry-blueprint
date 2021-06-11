import { TemplateRef } from '@angular/core';

export interface ColumnDef {
  field: string;
  header?: string;
  hide?: boolean;
  disabled?: boolean;
  pinned?: 'left' | 'right';
  width?: string;
  sortable?: boolean | string;
  sortProp?: ColumnSortProp;
  type?: ColumnType;
  typeParameter?: ColumnTypeParameter;
  formatter?: (rowData: any, colDef?: ColumnDef) => void;
  cellTemplate?: TemplateRef<any> | null;
  class?: string;
  i18nKey?: string;
}

export declare type ColumnType =
  | 'boolean'
  | 'number'
  | 'currency'
  | 'percent'
  | 'date'
  | 'link';

export interface ColumnTypeParameter {
  currencyCode?: string;
  display?: string | boolean;
  digitsInfo?: string;
  format?: string;
  locale?: string;
  timezone?: string;
}

export interface ColumnSortProp {
  id?: string;
  start?: 'asc' | 'desc';
  disableClear?: boolean;
}

export interface CellTemplate {
  [key: string]: TemplateRef<any>;
}

export interface RowSelectionFormatter {
  disabled?: (rowData: any, index?: number) => boolean;
  hideCheckbox?: (rowData: any, index?: number) => boolean;
}

export interface RowClassFormatter {
  [className: string]: (rowData: any, index?: number) => boolean;
}

export interface PagedData<T> {
  items?: T[];
  pageSize?: number;
  pageNumber?: number;
  totalCount?: number;
  totalPages?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;
}
