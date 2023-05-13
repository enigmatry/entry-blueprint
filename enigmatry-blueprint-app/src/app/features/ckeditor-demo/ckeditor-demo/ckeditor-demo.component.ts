import { Location } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-ckeditor-demo',
  templateUrl: './ckeditor-demo.component.html',
  styleUrls: ['./ckeditor-demo.component.scss']
})
export class CkeditorDemoComponent {
  model = {};

  constructor(public location: Location) { }

  save(value: { description?: string }) {
    // eslint-disable-next-line no-alert
    alert(value.description);
  }
}
