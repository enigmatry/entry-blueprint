import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { EntryAlertDialogComponent } from './dialogs/alert/entry-alert-dialog.component';
import { EntryConfirmDialogComponent } from './dialogs/confirm/entry-confirm-dialog.component';
import { EntryDialogComponent } from './dialogs/entry-dialog.component';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    EntryDialogComponent,
    EntryAlertDialogComponent,
    EntryConfirmDialogComponent
  ],
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule
  ],
  exports: [
    MatDialogModule
  ]
})
export class EntryDialogModule { }
