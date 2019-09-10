import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  apiUrl = environment.apiUrl;

  constructor(
    private titleService: Title,
    private translate: TranslateService,
    private localeService: BsLocaleService) {
    this.translate.setDefaultLang('nl');
    this.localeService.use('nl');
  }

  setTitle(newTitle: string) {
    this.titleService.setTitle(newTitle);
  }

  public useLanguage(language: string) {
    this.translate.use(language);
    this.localeService.use(language);
  }
}
