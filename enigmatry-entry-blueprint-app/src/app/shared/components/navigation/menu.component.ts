import { Component, computed, effect, inject, ViewChild } from '@angular/core';
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

  private readonly currentUserService: CurrentUserService = inject(CurrentUserService);
  private readonly permissionService: PermissionService = inject(PermissionService);
  private readonly authService: AuthService = inject(AuthService);
  readonly sizeService: SizeService = inject(SizeService);

  get currentUser() {
    return this.currentUserService.currentUser;
  }

  menuRole = computed(() => {
    if (this.sizeService.lastKnownSize()?.supportsSideMenu) {
      return 'navigation';
    }

    return 'dialog';
  });

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

  constructor() {
    effect(async() => {
      this.menuRole();
      await this.drawer?.close();
    });
  }

  readonly toggleDrawer = async() => {
    await this.drawer.toggle();
  };

  readonly show = (menuItem: { permission: PermissionId }) =>
    this.permissionService.hasPermissions([menuItem.permission]) || menuItem.permission === PermissionId.None;

  onLogout = () => {
    this.authService.logout();
  };
}
