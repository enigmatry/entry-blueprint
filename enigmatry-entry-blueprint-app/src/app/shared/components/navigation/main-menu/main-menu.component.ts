import { CommonModule } from '@angular/common';
import { Component, computed, inject, input } from '@angular/core';
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
  readonly menuItems = input.required<{ description: string; icon: string; aria: string; url: string; permission: PermissionId }[]>();
  readonly onHamburgerClick = input.required<() => void>();
  readonly onLogout = input.required<() => void>();
  readonly show = input.required<(menuItem: { permission: PermissionId }) => void>();
  readonly currentUser = input<UserProfile | null>(null);

  private readonly sizeService: SizeService = inject(SizeService);
  readonly permissionService: PermissionService = inject(PermissionService);

  readonly showSideMenu = computed(() => {
    return this.sizeService.lastKnownSize()?.supportsSideMenu ?? false;
  });
}
