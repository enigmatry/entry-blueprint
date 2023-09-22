import { Locator, Page } from "@playwright/test";
import { testData } from "../playwright.config";

export class LoginPage{
    private readonly baseUrl: string;
    readonly page: Page;
    readonly emailInput: Locator;
    readonly passwordInput: Locator;
    readonly submitButton: Locator;
    readonly userNameButton: Locator;

    constructor(page: Page){
        this.page = page;
        this.baseUrl = testData.baseUrl;
        this.emailInput = page.getByPlaceholder('E-mailadres');
        this.passwordInput = page.getByPlaceholder('Wachtwoord');
        this.submitButton = page.getByRole('button', { name: 'Aanmelden' });
        this.userNameButton = page.getByTestId('username-button');
    }

    async login(){
        await this.page.goto(this.baseUrl);
        await this.emailInput.fill(testData.loginEmail);
        await this.passwordInput.fill(testData.loginPassword);
        await this.submitButton.click();
    }
}