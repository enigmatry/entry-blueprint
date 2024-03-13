import { Component, Input } from '@angular/core';
import { PermissionId } from '@api';
import { UserProfile } from '@app/auth/user-profile';

@Component({
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
