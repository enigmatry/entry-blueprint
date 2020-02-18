import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationComponent } from './translation/translation.component';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
  { path: 'translation', component: TranslationComponent, data: {title: 'Translation'} },
  { path: 'users', loadChildren: () => import('./usermanager/usermanager.module').then(m => m.UserManagerModule) },
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
