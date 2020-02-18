import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationComponent } from './translation/translation.component';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
  { path: 'translation', component: TranslationComponent, data: {title: 'Translation'} },
  { path: 'users', loadChildren: './usermanager/usermanager.module#UserManagerModule' },
  { path: '', redirectTo: 'users', pathMatch: 'full' }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule, RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
