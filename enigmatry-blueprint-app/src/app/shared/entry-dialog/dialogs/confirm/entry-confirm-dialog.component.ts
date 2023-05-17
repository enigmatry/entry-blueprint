import { Component, Inject } from '@angular/core';
import { IEntryDialogButtonsConfig } from '../../models/entry-dialog-buttons-config.interface';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EntryDialogComponent } from '../entry-dialog.component';
import { IEntryConfirmDialogData } from '../../models/entry-confirm-dialog-data.interface';
import { EntryDialogLocalization } from '../../models/entry-dialog-localization';

@Component({
  selector: 'app-entry-confirm-dialog',
  templateUrl: './entry-confirm-dialog.component.html',
  styleUrls: ['./entry-confirm-dialog.component.scss']
})
export class EntryConfirmDialogComponent extends EntryDialogComponent {
  readonly buttons: IEntryDialogButtonsConfig = {
    alignment: 'align-right',
    submit: this.data.confirmText ?? EntryDialogLocalization.confirm,
    cancel: this.data.cancelText ?? EntryDialogLocalization.cancel,
    visible: true
  };

  constructor(
    protected readonly mdDialogRef: MatDialogRef<EntryConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) readonly data: IEntryConfirmDialogData) {
    super(mdDialogRef);
  }
}
