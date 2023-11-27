import { LayoutModule } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { MainMenuComponent } from './main-menu/main-menu.component';
import { MenuComponent } from './menu.component';
import { SideMenuComponent } from './side-menu/side-menu.component';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatMenuModule,
    MatListModule
  ],
  exports: [
    MenuComponent,
    MainMenuComponent,
    SideMenuComponent
  ],
  declarations: [
    MainMenuComponent, SideMenuComponent, MenuComponent
  ]
})
export class NavigationComponentsModule {}
