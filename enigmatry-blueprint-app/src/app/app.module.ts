import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AccordionModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
