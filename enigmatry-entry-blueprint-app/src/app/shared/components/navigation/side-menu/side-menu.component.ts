import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatListItem, MatNavList } from '@angular/material/list';
import { MatToolbar } from '@angular/material/toolbar';
import { RouterLink } from '@angular/router';
import { PermissionId } from '@api';
import { UserProfile } from '@app/auth/user-profile';

@Component({
  standalone: true,
  imports: [MatNavList, MatListItem, MatToolbar, MatIcon, CommonModule, RouterLink],
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent {
  showUserActions = false;
  @Input() show: (menuItem: { permission: PermissionId }) => void;
  @Input() menuItems: { description: string; icon: string; aria: string; url: string; permission: PermissionId }[];
  @Input() onLogout: () => void;
  @Input() currentUser: UserProfile | null;

  readonly toggleUserActions = ($event: Event) => {
    $event.stopImmediatePropagation();
    this.showUserActions = !this.showUserActions;
  };
}
