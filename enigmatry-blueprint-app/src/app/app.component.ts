import { Component } from '@angular/core';
import { EntryDialogService } from './shared/entry-dialog/services/entry-dialog.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'enigmatry-blueprint-app';

  constructor(private entryDialog: EntryDialogService) {}

  openAlertDialog = () =>
    this.entryDialog.openAlert({
      title: 'ALERT',
      message: `Lorem Ipsum is simply dummy text of the printing and typesetting industry. `
      + `Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, `
      + `when an unknown printer took a galley of type and scrambled it to make a type specimen book.`
    });

  openConfirmDialog = () =>
    this.entryDialog.openConfirm({
      title: 'CONFIRM',
      message: `Lorem Ipsum is simply dummy text of the printing and typesetting industry. `
      + `Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, `
      + `when an unknown printer took a galley of type and scrambled it to make a type specimen book.`
    })
    // eslint-disable-next-line no-alert
    .subscribe(confirmed => alert(confirmed));

  openFormDialog = () => {
    throw new Error('NOT IMPLEMENTED!');
  };
}
