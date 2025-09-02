import { Component, input, OnInit, TemplateRef, viewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { EntryButtonModule, EntryValidationModule } from '@enigmatry/entry-components';
import { GeneratedFormComponent } from '../form-component/generated-form-component.model';

@Component({
  imports: [EntryValidationModule, MatButtonModule, EntryButtonModule],
  selector: 'app-form-wrapper',
  templateUrl: './form-wrapper.component.html',
  styleUrls: ['./form-wrapper.component.scss']
})
export class FormWrapperComponent<T> implements OnInit {
  readonly formComponent = input.required<GeneratedFormComponent<T>>();
  readonly defaultFormButtonsTpl = viewChild<TemplateRef<unknown>>('defaultFormButtonsTpl');

  ngOnInit(): void {
    if (this.formComponent()) {
      this.setDefaultFormButtonsTemplate();
    }
  }

  private setDefaultFormButtonsTemplate() {
    if (!this.formComponent().formButtonsTemplate) {
      this.formComponent().formButtonsTemplate = this.defaultFormButtonsTpl();
    }
  }
}
