import { Component } from '@angular/core';
import { PermissionId } from 'src/app/api/api-reference';
import { AuthService } from 'src/app/core/auth/auth.service';
import { CurrentUserService } from 'src/app/core/services/current-user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  PermissionId = PermissionId;
  currentUser = this.currentUserService.currentUser;

  constructor(private currentUserService: CurrentUserService, private authService: AuthService) {}

  onLogout() {
    this.authService.logout();
  }
}
