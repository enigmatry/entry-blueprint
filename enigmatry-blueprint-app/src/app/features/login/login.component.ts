import { Component, OnInit } from '@angular/core';
import { MsalAuthService } from 'src/app/core/auth/azure-ad-b2c/msal-auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  isAuthenticated: boolean;
  username: string;

  constructor(private authService: MsalAuthService) {
  }

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    this.username = this.isAuthenticated ? this.authService.getAccount().username : '';
  }

  onLogin() {
    this.authService.loginRedirect();
  }

  onLogout() {
    this.authService.logout();
  }
}
