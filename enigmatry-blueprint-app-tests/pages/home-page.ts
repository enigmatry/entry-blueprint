import { Selector, t } from 'testcafe';
import { getBaseUrl } from '../config/testcafe-config';

export default class HomePage {
  public header = Selector('h1')
  public subHeader = Selector('h2')
  public useLanguageButton = Selector('.btn.btn-primary');
  public datePickerInput = Selector('input[id="username"]');
  public dateRangePickerInput = Selector('input[id="password"]');
  public setTitleButtons = Selector('.btn i.icon-loader');
  public url: string;
  constructor() {
    this.url = getBaseUrl();
  }
  public async selectLanguage(language: 'Dutch' | 'English') {
    const index = language === 'English' ? 0 : 1
    await t.click(this.useLanguageButton.nth((index)))
  }
}
