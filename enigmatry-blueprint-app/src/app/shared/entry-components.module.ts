import { ModuleWithProviders, NgModule } from '@angular/core';
import {
  ENTRY_SEARCH_FILTER_CONFIG, EntryDialogModule, EntryFileInputModule, EntrySearchFilterConfig,
  EntrySearchFilterModule
} from '@enigmatry/entry-components';
import { ENTRY_BUTTON_CONFIG, EntryButtonConfig, EntryButtonModule } from '@enigmatry/entry-components/button';
import { EntryPermissionModule, EntryPermissionService } from '@enigmatry/entry-components/permissions';
import { EntryTableModule } from '@enigmatry/entry-components/table';
import { EntryValidationModule } from '@enigmatry/entry-components/validation';
import { PermissionService } from '../core/auth/permissions.service';

@NgModule({
  declarations: [],
  exports: [
    EntryButtonModule,
    EntryDialogModule,
    EntryFileInputModule,
    EntryValidationModule,
    EntryPermissionModule,
    EntrySearchFilterModule,
    EntryTableModule
  ],
  providers: [
    {
      provide: ENTRY_BUTTON_CONFIG,
      useValue: new EntryButtonConfig({
        submit: { type: 'raised', color: 'primary' },
        cancel: { type: 'basic' }
      })
    },
    {
      provide: ENTRY_SEARCH_FILTER_CONFIG,
      useFactory: () => new EntrySearchFilterConfig({
        applyButtonText: 'Search'
      })
    }
  ]
})
export class EntryComponentsModule {
  static forRoot(): ModuleWithProviders<EntryComponentsModule> {
    return {
      ngModule: EntryComponentsModule,
      providers: [
        {
          provide: EntryPermissionService,
          useClass: PermissionService
        }
      ]
    };
  }
}
