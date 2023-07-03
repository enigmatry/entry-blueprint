// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl: 'https://localhost:44394',
  azureAdB2C: {
    clientId: 'a8793ce9-86dc-4d7e-aa70-361a3c5a5150',
    instance: 'https://enigmatryb2cdev.b2clogin.com',
    domain: 'enigmatryb2cdev.onmicrosoft.com',
    signUpSignInPolicyId: 'B2C_1_entry_demo_sign_in',
    // eslint-disable-next-line no-secrets/no-secrets
    scopes: ['https://enigmatryb2cdev.onmicrosoft.com/a8793ce9-86dc-4d7e-aa70-361a3c5a5150/api']
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// Import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
