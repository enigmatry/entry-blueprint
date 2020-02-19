
import HomePage from "../pages/home-page";

const page = new HomePage();

fixture('Home page')
  .page(page.url)

test('Dutch language selection', async t => {
  await page.selectLanguage('Dutch')
  await t
    .expect(page.subHeader.innerText)
    .eql('Dit is een voorbeeld Angular app voor Enigmatry-projecten');
});

test("English language selection", async t => {
  await page.selectLanguage('English')
  await t
    .expect(page.subHeader.innerText)
    .eql('This is a sample Angular app for Enigmatry projects');
});

// test("Static header content", async t => {
//   await page.clickStaticHeader()
//   await t
//     .expect(page.staticHeaderContent.textContent)
//     .eql(' This content is straight in the template. ');
// });

// test("Date input", async t => {   
//   await page.inputDatePicker(('15-08-2019'))
//   await t
//     .expect(page.datePickerInput.value)
//     .eql('15-08-2019');
// });


