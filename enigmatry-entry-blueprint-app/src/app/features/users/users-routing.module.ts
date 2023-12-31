import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RouteSegments } from 'src/app/shared/model/route-segments';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserListComponent } from './user-list/user-list.component';

const routes: Routes = [
  { path: '', component: UserListComponent },
  { path: `${RouteSegments.edit}/:id`, component: UserEditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }

