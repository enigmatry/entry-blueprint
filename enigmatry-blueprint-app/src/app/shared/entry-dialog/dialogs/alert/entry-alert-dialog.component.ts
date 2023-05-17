import { Component, Inject } from '@angular/core';
import { EntryDialogComponent } from '../entry-dialog.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { IEntryAlertDialogData } from '../../models/entry-alert-dialog-data.interface';
import { IEntryDialogButtonsConfig } from '../../models/entry-dialog-buttons-config.interface';
import { EntryDialogLocalization } from '../../models/entry-dialog-localization';

@Component({
  selector: 'app-entry-alert-dialog',
  templateUrl: './entry-alert-dialog.component.html',
  styleUrls: ['./entry-alert-dialog.component.scss']
})
export class EntryAlertDialogComponent extends EntryDialogComponent {
  readonly buttons: IEntryDialogButtonsConfig = {
    alignment: 'align-center',
    submit: this.data.confirmText ?? EntryDialogLocalization.confirm(),
    cancel: '',
    visible: true
  };

  constructor(
    protected readonly mdDialogRef: MatDialogRef<EntryAlertDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IEntryAlertDialogData) {
    super(mdDialogRef);
  }
}
