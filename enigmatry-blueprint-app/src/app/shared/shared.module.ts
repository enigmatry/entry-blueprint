import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { GridCellsModule } from './grid-cells/grid-cells.module';
import { DEFAULT_DATE_FORMAT, EnigmatryGridModule } from '@enigmatry/angular-building-blocks';

@NgModule({
    imports: [
        CommonModule
    ],
    exports: [
        FormsModule,
        ReactiveFormsModule,
        MaterialModule,
        GridCellsModule,
        EnigmatryGridModule
    ],
    providers: [
        {
            provide: DEFAULT_DATE_FORMAT,
            useFactory: () => $localize`:@@common.date-format:dd. MMM yyyy. 'at' hh:mm a`
        }
    ]
})
export class SharedModule { }
