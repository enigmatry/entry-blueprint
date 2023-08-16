import { NgModule } from '@angular/core';
import { ENTRY_BUTTON_CONFIG, EntryButtonConfig, EntryButtonModule } from '@enigmatry/entry-components/button';
import { EntryValidationModule } from '@enigmatry/entry-components/validation';
import { EntryTableModule } from '@enigmatry/entry-table';

@NgModule({
  declarations: [],
  exports: [
    EntryButtonModule,
    EntryTableModule,
    EntryValidationModule
  ],
  providers: [
    {
      provide: ENTRY_BUTTON_CONFIG,
      useValue: new EntryButtonConfig({
        submit: { type: 'raised', color: 'primary' },
        cancel: { type: 'basic' }
      })
    }
  ]
})
export class EntryComponentsModule { }
