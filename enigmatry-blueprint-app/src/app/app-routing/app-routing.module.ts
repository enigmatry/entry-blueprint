import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from '../home/home.component';
import { UsersComponent } from '../users/users.component';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
  { path: 'home', component: HomeComponent, data: {title: 'Home'} },
  { path: 'users', component: UsersComponent, data: {title: 'Users'} },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule, RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
