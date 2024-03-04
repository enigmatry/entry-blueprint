import { Component, Input } from '@angular/core';
import { PermissionId } from 'src/app/api/api-reference';
import { PermissionService } from 'src/app/core/auth/permissions.service';
import { UserProfile } from 'src/app/core/auth/user-profile';
import { SizeService } from 'src/app/core/services/size.service';

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.scss']
})
export class MainMenuComponent {
  @Input() menuItems: { description: string; icon: string; aria: string; url: string; permission: PermissionId }[];
  @Input() onHamburgerClick: () => void;
  @Input() onLogout: () => void;
  @Input() show: (menuItem: { permission: PermissionId }) => void;
  @Input() currentUser: UserProfile | null;

  get showSideMenu(): boolean {
    return this.sizeService.lastKnownSize.supportsSideMenu;
  }

  constructor(readonly sizeService: SizeService, readonly permissionService: PermissionService) { }
}
