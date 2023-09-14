import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GetUserProfileResponse } from 'src/app/api/api-reference';
import { AuthService } from 'src/app/core/auth/auth.service';
import { CurrentUserService } from 'src/app/core/services/current-user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  $currentUser: Observable<GetUserProfileResponse | null>;

  constructor(private currentUserService: CurrentUserService, private authService: AuthService) {
  }

  ngOnInit(): void {
    this.$currentUser = this.currentUserService.getUser();
  }

  onLogout() {
    this.authService.logout();
  }
}
