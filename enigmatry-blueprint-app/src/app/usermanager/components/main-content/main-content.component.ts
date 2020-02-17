import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../../services/user.service'
import { Observable } from 'rxjs';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.scss']
})
export class MainContentComponent implements OnInit {

  user: User;
  constructor(private route: ActivatedRoute, private service: UserService) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      let id = p['id'];
      if (!id) {
        id = '0118170f-b84f-4cae-c74a-08d7a5280e6e';
      }

      // only retrieve user when the service has retrieved any users
      this.service.getUsers().subscribe(users => {
        if (users.length === 0) return;

        // Just to show the spinner!
        this.user = null;
        setTimeout(() => {
          this.service.userById(id).subscribe(
            data => {
              this.user = data;
            },
            err => console.error(err)
          )
        }, 500);
      });
    });
  }

}
