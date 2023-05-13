import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CkeditorDemoRoutingModule } from './ckeditor-demo-routing.module';
import { CkeditorDemoComponent } from './ckeditor-demo/ckeditor-demo.component';
import { CkeditorDemoGeneratedModule } from './generated/ckeditor-demo-generated.module';
import { ENTRY_CKEDITOR_OPTIONS, FormlyCkeditorModule } from '@enigmatry/entry-form';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    CkeditorDemoComponent
  ],
  imports: [
    CommonModule,
    CkeditorDemoGeneratedModule,
    SharedModule,
    FormlyCkeditorModule,
    CkeditorDemoRoutingModule
  ],
  providers: [
    {
      provide: ENTRY_CKEDITOR_OPTIONS,
      useValue: {
        build: ClassicEditor,
        config: {
          toolbar: ['bold', 'italic', 'bulletedList', 'numberedList', 'blockQuote', 'link']
        }
      }
    }]
})
export class CkeditorDemoModule { }
