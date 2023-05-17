import { IEntryAlertDialogData } from './entry-alert-dialog-data.interface';

export interface IEntryConfirmDialogData extends IEntryAlertDialogData {
    cancelText?: string
}