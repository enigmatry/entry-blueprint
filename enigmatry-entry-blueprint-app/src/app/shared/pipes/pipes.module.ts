import { NgModule } from '@angular/core';
import { NotNullPipe } from './not-null.pipe';

@NgModule({
    imports: [
        NotNullPipe
    ],
    exports: [
        NotNullPipe
    ]
})
export class PipesModule { }