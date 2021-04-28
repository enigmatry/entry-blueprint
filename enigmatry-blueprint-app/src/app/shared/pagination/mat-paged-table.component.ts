import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { MatPagedDataSource } from './mat-paged-data-source';
import { PagedQueryParams } from './paged-data.model';

@Component({
  template: ''
})
export abstract class MatPagedTableComponent<T> implements AfterViewInit, OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<T>;
  dataSource: MatPagedDataSource<T> = new MatPagedDataSource<T>();
  queryParams: PagedQueryParams = new PagedQueryParams();
  pageSizeOptions = [10, 20, 50];

  constructor(protected router: Router, protected activatedRoute: ActivatedRoute) { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.dataSource.router = this.router;
    this.dataSource.activatedRoute = this.activatedRoute;
    this.dataSource.queryParams = this.queryParams;

    // connect data source
    this.table.dataSource = this.dataSource;
  }
}
