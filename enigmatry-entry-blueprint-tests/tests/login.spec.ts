import { test, expect } from '@playwright/test';
import { LoginPage } from '../pages/login-page';
import { testData } from '../playwright.config';

test('login', async ({ page }) => {
  const loginPage = new LoginPage(page);
  await loginPage.login();
  await expect(loginPage.userNameButton).toHaveText(testData.loginUsername);
});