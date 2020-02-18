import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { User } from '../shared/user.model';
import { FormControl, Validators } from '@angular/forms';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-new-contact-dialog',
  templateUrl: './new-contact-dialog.component.html',
  styleUrls: ['./new-contact-dialog.component.scss']
})
export class NewContactDialogComponent implements OnInit {

  user: User;
  constructor(private dialogRef: MatDialogRef<NewContactDialogComponent>, private userService: UserService) { }

  name = new FormControl('', [Validators.required]);
  userName = new FormControl('');
  createdDate = new FormControl('');

  getErrorMessage() {
    return this.name.hasError('required') ? 'You must enter a name' : '';
  }

  ngOnInit() {
    this.user = new User();
  }

  save() {
    const newUser = new User();
    newUser.name = this.name.value;
    newUser.userName = this.userName.value;
    newUser.createdOn = this.createdDate.value;

    this.userService.addUser(newUser).then(user => {
      this.dialogRef.close(newUser);
    });
  }

  dismiss() {
    this.dialogRef.close(null);
  }

}
