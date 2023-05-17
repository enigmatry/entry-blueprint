import { EntryDialogLocalization } from './entry-dialog-localization';

export interface IEntryDialogButtonsConfig {
    alignment: 'align-right' | 'align-center' | '',
    submit?: string,
    cancel?: string,
    visible: boolean;
}

const DEFAULT_ENTRY_DIALOG_BUTTONS_CONFIG = {
    alignment: 'align-right',
    submit: EntryDialogLocalization.confirm(),
    cancel: EntryDialogLocalization.cancel(),
    visible: true
} as IEntryDialogButtonsConfig;

export {
    DEFAULT_ENTRY_DIALOG_BUTTONS_CONFIG
};