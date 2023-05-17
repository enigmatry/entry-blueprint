import { Component, HostListener, Input, TemplateRef } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { IEntryDialogButtonsConfig } from '../models/entry-dialog-buttons-config.interface';
import { EntryDialogLocalization } from '../models/entry-dialog-localization';

@Component({
    selector: 'app-entry-dialog',
    templateUrl: './entry-dialog.component.html',
    styleUrls: ['./entry-dialog.component.scss']
})
export class EntryDialogComponent {
    @Input() title: string;
    @Input() buttons = {
        alignment: 'align-right',
        submit: EntryDialogLocalization.confirm,
        cancel: EntryDialogLocalization.cancel,
        visible: true
    } as IEntryDialogButtonsConfig;

    @Input() confirm: () => Observable<unknown> = () => of(true);
    @Input() cancel = () => this.close(false);
    @Input() disableConfirm = false;
    @Input() buttonsTemplate: TemplateRef<any> | null | undefined;

    constructor(protected readonly mdDialogRef: MatDialogRef<EntryDialogComponent>) { }

    readonly onSubmit = () => {
        this.confirm().subscribe({
            next: closeDialog => {
                if (closeDialog) {
                    this.close(closeDialog);
                }
            }
        });
    };

    @HostListener('keydown.esc')
    readonly onEsc = () => this.cancel();

    readonly close = (value: unknown = true) => this.mdDialogRef.close(value);
}
