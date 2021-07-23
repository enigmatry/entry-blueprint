import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { switchMap } from 'rxjs/operators';
import { IGetUserDetailsResponse, UserCreateOrUpdateCommand, UsersClient } from 'src/app/api/api-reference';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {

  model: IGetUserDetailsResponse = {};

  constructor(private client: UsersClient, protected activatedRoute: ActivatedRoute, public location: Location) { }

  ngOnInit(): void {
    this.activatedRoute.params
      .pipe(switchMap(params => this.client.get(params.id)))
      .subscribe(response => this.model = response);
  }

  save(model: IGetUserDetailsResponse) {
    const command = new UserCreateOrUpdateCommand({
      id: model.id,
      userName: model.userName,
      name: model.name
    });
    this.client.post(command)
      .subscribe(() => this.location.back());
  };

  cancel() { this.location.back(); }

}
