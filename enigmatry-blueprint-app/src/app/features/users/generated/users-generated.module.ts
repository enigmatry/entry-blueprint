// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------;
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { EnigmatryGridModule } from '@enigmatry/angular-building-blocks/enigmatry-grid';
    import { FormlyModule, FORMLY_CONFIG } from '@ngx-formly/core';
    import { FormlyMaterialModule } from '@ngx-formly/material';
    import { MatNativeDateModule } from '@angular/material/core';
    import { MatAutocompleteModule } from '@angular/material/autocomplete';
    import { FormlyMatDatepickerModule } from '@ngx-formly/material/datepicker';

import { UserEditGeneratedComponent } from './user-edit/user-edit-generated.component';
import { UserListGeneratedComponent } from './user-list/user-list-generated.component';



@NgModule({
    declarations: [UserEditGeneratedComponent, UserListGeneratedComponent],
    imports: [
    CommonModule,
    SharedModule,
    EnigmatryGridModule,
        MatAutocompleteModule,
        MatNativeDateModule,
        FormlyModule.forChild(
            {
                validationMessages: [
{ name: 'required', message: (err, field) =>  $localize `:@@validators.required:${field?.templateOptions?.label}:property-name: is required` },
{ name: 'minlength', message: (err, field) =>  $localize `:@@validators.minLength:${field?.templateOptions?.label}:property-name: should have at least ${field?.templateOptions?.minLength}:min-value: characters` },
{ name: 'maxlength', message: (err, field) =>  $localize `:@@validators.maxLength:${field?.templateOptions?.label}:property-name: value should be less than ${field?.templateOptions?.maxLength}:max-value: characters` }
                ]
            }
        ),
        FormlyMaterialModule,
        FormlyMatDatepickerModule,
    ],
    exports: [UserEditGeneratedComponent, UserListGeneratedComponent],
    providers: [
    ]
    })
    export class UsersGeneratedModule { }
