import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  ngOnInit() {
    this.titleService.setTitle('Blueprint')
  }

  constructor(
    private titleService: Title,
    private translate: TranslateService) {
    this.translate.setDefaultLang('nl');
  }
}
