import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CkeditorDemoComponent } from './ckeditor-demo/ckeditor-demo.component';

const routes: Routes = [{
  path: '',
  component: CkeditorDemoComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CkeditorDemoRoutingModule { }
