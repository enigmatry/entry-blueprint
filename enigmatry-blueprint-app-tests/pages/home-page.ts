import { Selector, t } from 'testcafe';
import { getBaseUrl } from '../config/testcafe-config';

export default class HomePage {
  public header = Selector('h1')
  public subHeader = Selector('h2')
  public useLanguageButton = Selector('button');
  public datePickerInput = Selector('input.bsdatepicker');
  public dateRangePickerInput = Selector('input.bsdaterangepicker');
  public setTitleButtons = Selector('.btn i.icon-loader');
  public url: string;
  constructor() {
    this.url = getBaseUrl();
  }
  public async selectLanguage(language: 'Dutch' | 'English') {
    const index = language === 'English' ? 0 : 1
    await t.click(this.useLanguageButton.nth(index))
  }
}
