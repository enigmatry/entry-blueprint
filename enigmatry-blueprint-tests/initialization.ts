import dotenv from 'dotenv';
import dotenvExpand from 'dotenv-expand';
import path from 'path';
import { TestData } from './test-data/test-data.model';

export const initEnvironment = () => {
    let env = process.env.PLAYWRIGHT_ENVIRONMENT ?? 'dev';
    let myEnv = dotenv.config({ path: path.resolve(__dirname, `env/${env}.env`) });
    dotenvExpand.expand(myEnv);
    return {
        baseUrl: process.env.BASE_URL,
        loginEmail: process.env.LOGIN_EMAIL,
        loginPassword: process.env.LOGIN_PASSWORD,
        loginUsername: process.env.LOGIN_USERNAME
    } as TestData
}