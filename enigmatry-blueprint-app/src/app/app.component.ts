import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { ApplicationInsightsService } from './services/application-insights.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private appInsights;

  constructor(
    private titleService: Title,
    private translate: TranslateService,
    private router: Router) {
      this.translate.setDefaultLang('nl');
      this.appInsights = new ApplicationInsightsService(router);
  }

  ngOnInit() {
    this.titleService.setTitle('Blueprint');
  }
}
