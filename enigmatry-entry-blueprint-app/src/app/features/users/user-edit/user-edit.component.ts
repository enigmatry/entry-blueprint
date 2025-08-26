import { Location } from '@angular/common';
import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  CreateOrUpdateUserCommand, IGetUserDetailsResponse,
  IValidationProblemDetails,
  LookupResponseOfGuid, LookupResponseOfUserStatusId, PermissionId, UsersClient
} from '@api';
import { setServerSideValidationErrors } from '@enigmatry/entry-components';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { UserEditGeneratedComponent } from '../generated/user-edit/user-edit-generated.component';

@Component({
  standalone: false,
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  @ViewChild('formComponent') formComponent: UserEditGeneratedComponent;

  model: IGetUserDetailsResponse = {};
  $roleLookup: Observable<LookupResponseOfGuid[]>;
  $userStatusLookup: Observable<LookupResponseOfUserStatusId[]>;
  PermissionId = PermissionId;

  private readonly client: UsersClient = inject(UsersClient);
  private readonly activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  readonly location: Location = inject(Location);

  ngOnInit(): void {
    this.activatedRoute.params
      .pipe(switchMap(params => this.client.get(params['id'])))
      .subscribe(response => {
        this.model = response;
      });

    this.$roleLookup = this.client.getRolesLookup();
    this.$userStatusLookup = this.client.getStatusesLookup();
  }

  save(model: IGetUserDetailsResponse) {
    const command = new CreateOrUpdateUserCommand({
      id: model.id,
      emailAddress: model.emailAddress,
      roleId: model.roleId,
      userStatusId: model.userStatusId,
      fullName: model.fullName
    });
    this.client.post(command)
      .subscribe({
        next: () => this.location.back(),
        error: (error: IValidationProblemDetails) => {
          setServerSideValidationErrors(error, this.formComponent.form);
        }
      });
  }

  cancel() {
    this.location.back();
  }
}
