import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import {
  CreateOrUpdateUserCommand, IGetUserDetailsResponse,
  LookupResponseOfGuid, UsersClient
} from 'src/app/api/api-reference';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  model: IGetUserDetailsResponse = {};
  $roleLookup: Observable<LookupResponseOfGuid[]>;

  constructor(private client: UsersClient, protected activatedRoute: ActivatedRoute, public location: Location) { }

  ngOnInit(): void {
    this.activatedRoute.params
      .pipe(switchMap(params => this.client.get(params.id)))
      .subscribe(response => {
        this.model = response;
      });

    this.$roleLookup = this.client.getRolesLookup();
  }

  save(model: IGetUserDetailsResponse) {
    const command = new CreateOrUpdateUserCommand({
      id: model.id,
      emailAddress: model.emailAddress,
      roleId: model.roleId,
      fullName: model.fullName
    });
    this.client.post(command)
      .subscribe(() => this.location.back());
  }

  cancel() {
    this.location.back();
  }
}
