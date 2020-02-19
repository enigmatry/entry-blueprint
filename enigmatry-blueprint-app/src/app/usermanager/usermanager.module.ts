import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../shared/material.module';
import { UserService } from './shared/user.service';
import { UserManagerRoutingModule } from './usermanager-routing.module';

import { MainContentComponent } from './main-content/main-content.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { UsermanagerAppComponent } from './usermanager-app.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { NewContactDialogComponent } from './new-contact-dialog/new-contact-dialog.component';

@NgModule({
  declarations: [
    MainContentComponent,
    SidenavComponent,
    UsermanagerAppComponent,
    ToolbarComponent,
    NewContactDialogComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    UserManagerRoutingModule
  ],
  providers: [
    UserService
  ],
  entryComponents: [
    NewContactDialogComponent
  ]
})
export class UserManagerModule { }
