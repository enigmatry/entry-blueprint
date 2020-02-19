import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsermanagerAppComponent } from './usermanager-app.component';
import { MainContentComponent } from './main-content/main-content.component';
import { UserResolver } from './shared/user.resolver';


const routes: Routes = [
  { path: '', component: UsermanagerAppComponent,
    children: [
      {
        path: ':id',
        component: MainContentComponent,
        resolve: {
          user: UserResolver
        }
      },
      { path: '', component: MainContentComponent }
    ] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserManagerRoutingModule { }
