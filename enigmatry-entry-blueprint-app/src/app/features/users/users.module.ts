import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';

import { UsersGeneratedModule } from './generated/users-generated.module';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserListComponent } from './user-list/user-list.component';
import { UsersRoutingModule } from './users-routing.module';

@NgModule({
  declarations: [
    UserListComponent,
    UserEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    UsersGeneratedModule,
    UsersRoutingModule
  ]
})
export class UsersModule { }
