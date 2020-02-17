import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../shared/material.module';
import { UserService } from '../services/user.service';
import { UserManagerRoutingModule } from './usermanager-routing.module';

import { MainContentComponent } from './components/main-content/main-content.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { UsermanagerAppComponent } from './usermanager-app.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { NewContactDialogComponent } from './components/new-contact-dialog/new-contact-dialog.component';

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
