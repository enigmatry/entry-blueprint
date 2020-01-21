import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { FormGroup, FormControl } from '@angular/forms';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.scss']
})
export class UserUpdateComponent implements OnInit {
  @Input() selectedUser: User;
  public userForm = new FormGroup({
    userName: new FormControl(''),
    createdDate: new FormControl('')
  });


  constructor(private userService: UserService) {}

  ngOnInit() {
    this.userForm = this.createForm();
  }

  private createForm(): FormGroup{
    return new FormGroup({
      userName: new FormControl(this.selectedUser.name),
      createdDate: new FormControl(this.selectedUser.createdOn)
    });
  }


  updateUser() {
    // if (this.selection.selected[0])
    // {
    //   this.selection.selected[0].name = this.selectedName;

    //   this.userService.updateUser(this.selection.selected[0]).subscribe(
    //     data => {
    //       console.log(data);
    //     },
    //     err => console.error(err),
    //     () => console.log('Done')
    //   );
    // }
  }
}
