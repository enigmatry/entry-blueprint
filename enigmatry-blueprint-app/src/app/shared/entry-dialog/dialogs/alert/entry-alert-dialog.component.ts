import { Component, Inject } from '@angular/core';
import { EntryDialogComponent } from '../entry-dialog.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { IEntryAlertDialogData } from '../../models/entry-alert-dialog-data.interface';
import { IEntryDialogButtonsConfig } from '../../models/entry-dialog-buttons-config.interface';
import { ENTRY_DIALOG_CONFIG, EntryDialogConfig } from '../../models/entry-dialog-config.model';

@Component({
  selector: 'app-entry-alert-dialog',
  templateUrl: './entry-alert-dialog.component.html',
  styleUrls: ['./entry-alert-dialog.component.scss']
})
export class EntryAlertDialogComponent extends EntryDialogComponent {
  readonly buttons: IEntryDialogButtonsConfig = {
    confirmButtonText: this.data.confirmText ?? this.config.confirmButtonText,
    cancelButtonText: '',
    buttonsAlignment: 'align-center',
    visible: true
  };

  constructor(
    protected readonly mdDialogRef: MatDialogRef<EntryDialogComponent>,
    @Inject(ENTRY_DIALOG_CONFIG) protected readonly config: EntryDialogConfig,
    @Inject(MAT_DIALOG_DATA) public data: IEntryAlertDialogData) {
    super(mdDialogRef, config);
  }
}
