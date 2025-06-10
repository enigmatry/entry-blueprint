import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatToolbar } from '@angular/material/toolbar';
import { RouterLink } from '@angular/router';
import { PermissionId } from '@api';
import { PermissionService } from '@app/auth/permissions.service';
import { UserProfile } from '@app/auth/user-profile';
import { SizeService } from '@services/size.service';

@Component({
  standalone: true,
  imports: [MatButton, MatToolbar, MatIcon, MatMenu, MatMenuTrigger, CommonModule, RouterLink],
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
    return this.sizeService.lastKnownSize()?.supportsSideMenu ?? false;
  }

  constructor(readonly sizeService: SizeService, readonly permissionService: PermissionService) { }
}
