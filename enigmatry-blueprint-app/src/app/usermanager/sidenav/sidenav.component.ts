import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/usermanager/shared/user.model';
import { UserService } from 'src/app/usermanager/shared/user.service';
import { MatSidenav } from '@angular/material';
import { Router } from '@angular/router';

const SMALL_WIDTH_BREAK = 720;

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {

  private mediaMatcher: MediaQueryList = matchMedia(`(max-width: ${SMALL_WIDTH_BREAK}px)`);

  users: Observable<User[]>;

  constructor(private userService: UserService, private router: Router) { }

  @ViewChild(MatSidenav, null) sidenav: MatSidenav;

  ngOnInit() {
    this.users = this.userService.getUsers();

    this.router.events.subscribe(() => {
      if (this.isScreenSmall()) {
        this.sidenav.close();
      }
    });
  }

  isScreenSmall(): boolean {
    return this.mediaMatcher.matches;
  }
}
