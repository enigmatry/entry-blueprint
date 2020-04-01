import { Selector, t } from 'testcafe';
import { getBaseUrl } from '../config/testcafe-config';

export default class TranslationPage {
  public header = Selector('h1')
  public subHeader = Selector('h2')
  public useLanguageButton = Selector('body > app-root > app-translation > div > button');
  public datePickerInput = Selector('body > app-root > div.row > div > input');
  public staticHeader = Selector("accordion-group[heading='Static Header, initially expanded']")
  public staticHeaderContent = Selector("div[class='panel-body card-block card-body']")
  public dateRangePickerInput = Selector('input.bsdaterangepicker');
  public setTitleButtons = Selector('.btn i.icon-loader');
  public url: string;

  constructor() {
    this.url = getBaseUrl() + '/translation';
  }

  public async selectLanguage(language: 'Dutch' | 'English') {
    const index = language === 'English' ? 0 : 1
    await t.click(this.useLanguageButton.nth(index))
  }

  public async inputDatePicker(date: string) {
    await t.typeText(this.datePickerInput, date)
  }

  public async clickStaticHeader() {
    await t.click(this.staticHeader)
  }
}
