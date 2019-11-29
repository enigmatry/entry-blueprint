import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../services/user.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { User } from '../models/user.model';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  displayedColumns = ['userName','name', 'createdOn'];
  users = new MatTableDataSource();
  selection = new SelectionModel<User>(false, []);
  selectedName: string;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private userService: UserService) {
  }

  ngOnInit() {
    this.getUsers();
    this.users.paginator = this.paginator;
    this.users.sort = this.sort;
    // This ensures case-insensitive sorting:
    this.users.sortingDataAccessor = (data, sortHeaderId) => data[sortHeaderId].toLocaleLowerCase();
  }

  getUsers() {
    this.userService.getUsers().subscribe(
      data => {
        console.log(data);
        this.users.data = data;
      },
      err => console.error(err),
      () => console.log('Done')
    )
  }

  applyFilter(filterValue: string) {
    this.users.filter = filterValue.trim().toLowerCase();

    if (this.users.paginator) {
      this.users.paginator.firstPage();
    }
  }

  rowSelected(row: any) {
    this.selection.toggle(row);
    this.selectedName = this.selection.selected[0].name;
  }

  updateUser() {
    if (this.selection.selected[0])
    {
      this.selection.selected[0].name = this.selectedName;

      this.userService.updateUser(this.selection.selected[0]).subscribe(
        data => {
          console.log(data);
        },
        err => console.error(err),
        () => console.log('Done')
      );
    }
  }
}
