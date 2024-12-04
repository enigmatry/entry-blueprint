import { Component, ViewChild } from '@angular/core';
import { MatSidenav, MatSidenavContainer, MatSidenavContent } from '@angular/material/sidenav';
import { PermissionId } from '@api';
import { AuthService } from '@app/auth/auth.service';
import { PermissionService } from '@app/auth/permissions.service';
import { CurrentUserService } from '@services/current-user.service';
import { SizeService } from '@services/size.service';
import { MainMenuComponent } from './main-menu/main-menu.component';
import { SideMenuComponent } from './side-menu/side-menu.component';

@Component({
  standalone: true,
  selector: 'app-menu',
  imports: [MatSidenavContent, MatSidenav, MatSidenavContainer, MainMenuComponent, SideMenuComponent],
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {
  @ViewChild('drawer') drawer: MatSidenav;

  get currentUser() {
    return this.currentUserService.currentUser;
  }

  menuItems = [
    { description: 'Home', icon: 'home', aria: 'Home icon', url: '/home', permission: PermissionId.None },
    {
      description: 'Users',
      icon: 'supervised_user_circle',
      aria: 'Users icon',
      url: '/users',
      permission: PermissionId.UsersRead
    },
    {
      description: 'Products',
      icon: 'category',
      aria: 'Products icon',
      url: '/products',
      permission: PermissionId.ProductsRead
    }
  ];

  get menuRole(): 'dialog' | 'navigation' {
    if (this.sizeService.lastKnownSize.supportsSideMenu) {
      return 'navigation';
    }

    this.drawer?.close();
    return 'dialog';
  }

  constructor(private readonly currentUserService: CurrentUserService,
    private readonly permissionService: PermissionService,
    private readonly authService: AuthService,
    readonly sizeService: SizeService) { }

  toggleDrawer = () => {
    this.drawer.toggle();
  };

  readonly show = (menuItem: { permission: PermissionId }) =>
    this.permissionService.hasPermissions([menuItem.permission]) || menuItem.permission === PermissionId.None;

  onLogout = () => {
    this.authService.logout();
  };
}
