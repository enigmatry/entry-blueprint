import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/usermanager/shared/user.model';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.scss']
})
export class MainContentComponent implements OnInit {

  user: User;
  constructor(private route: ActivatedRoute, private service: UserService) { }

  ngOnInit() {
    this.route.params.subscribe(routeData => {
      // In case we want to use the Route Resolver instead:
      //   this.user = routeData.user;
      // }, err => console.error(err)

      const idKey = 'id';
      let id = data[idKey];
      if (!id) {
        id = '0118170f-b84f-4cae-c74a-08d7a5280e6e';
      }

      // Just to show the spinner!
      this.user = null;
      setTimeout(() => {
        this.service.userById(id).subscribe(
          data => {
            this.user = data;
          },
          err => console.error(err)
        );
      }, 500);
    });
  }
}
