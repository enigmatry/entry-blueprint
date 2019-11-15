import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { environment } from '../../environments/environment';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  ngOnInit() {
  }

  apiUrl = environment.apiUrl;

  constructor(
    private titleService: Title,
    private translate: TranslateService) {
  }

  setTitle(newTitle: string) {
    this.titleService.setTitle(newTitle);
  }

  public useLanguage(language: string) {
    this.translate.use(language);
  }
}
