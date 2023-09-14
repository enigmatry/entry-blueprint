import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NotNullPipe } from './not-null.pipe';

@NgModule({
    declarations: [
        NotNullPipe
    ],
    imports: [
        CommonModule
    ],
    exports: [
        NotNullPipe
    ]
})
export class PipesModule { }