import { Component, computed, effect, inject, viewChild } from '@angular/core';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
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
  imports: [MatSidenavModule, MainMenuComponent, SideMenuComponent],
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {
  readonly drawer = viewChild<MatSidenav>('drawer');
  private readonly currentUserService: CurrentUserService = inject(CurrentUserService);
  private readonly permissionService: PermissionService = inject(PermissionService);
  private readonly authService: AuthService = inject(AuthService);
  readonly sizeService: SizeService = inject(SizeService);
  readonly currentUser = computed(() => this.currentUserService.currentUser());

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
    let lastRole: string | undefined;
    effect(async() => {
      const role = this.menuRole();
      if (role === 'dialog' && lastRole !== 'dialog') {
        await this.drawer()?.close();
      }
      // eslint-disable-next-line require-atomic-updates
      lastRole = role;
    });
  }

  readonly toggleDrawer = async() => {
    await this.drawer()?.toggle();
  };

  readonly show = (menuItem: { permission: PermissionId }) =>
    this.permissionService.hasPermissions([menuItem.permission]) || menuItem.permission === PermissionId.None;

  onLogout = () => {
    this.authService.logout();
  };
}
