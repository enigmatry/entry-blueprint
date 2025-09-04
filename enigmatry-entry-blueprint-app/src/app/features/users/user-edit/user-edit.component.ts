import { CommonModule, Location } from '@angular/common';
import { Component, inject, OnInit, viewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  CreateOrUpdateUserCommand, IGetUserDetailsResponse,
  IValidationProblemDetails,
  LookupResponseOfGuid, LookupResponseOfUserStatusId, PermissionId, UsersClient
} from '@api';
import { EntryPermissionModule, setServerSideValidationErrors } from '@enigmatry/entry-components';
import { FormWrapperComponent } from '@shared/form-wrapper/form-wrapper.component';
import { NotNullPipe } from '@shared/pipes/not-null.pipe';
import { lastValueFrom, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { UserEditGeneratedComponent } from '../generated/user-edit/user-edit-generated.component';
import { UsersGeneratedModule } from '../generated/users-generated.module';

@Component({
  imports: [CommonModule, FormWrapperComponent, EntryPermissionModule, UsersGeneratedModule, NotNullPipe],
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  readonly formComponent = viewChild<UserEditGeneratedComponent>('formComponent');

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

  readonly save = async(model: IGetUserDetailsResponse) => {
    const command = new CreateOrUpdateUserCommand({
      id: model.id,
      emailAddress: model.emailAddress,
      roleId: model.roleId,
      userStatusId: model.userStatusId,
      fullName: model.fullName
    });

    try {
      await lastValueFrom(this.client.post(command));
      this.location.back();
    } catch (error) {
      // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
      setServerSideValidationErrors(error as IValidationProblemDetails, this.formComponent()!.form);
    }
  };

  readonly cancel = () => {
    this.location.back();
  };
}
