// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  appVersion: '0.0.0',
  apiUrl: 'https://localhost:44394',
  azureAd: {
    clientId: 'a8793ce9-86dc-4d7e-aa70-361a3c5a5150',
    /* eslint-disable no-secrets/no-secrets */
    authority: 'https://enigmatryb2cdev.b2clogin.com/enigmatryb2cdev.onmicrosoft.com/B2C_1_entry_blueprint_sign_in',
    scopes: ['https://enigmatryb2cdev.onmicrosoft.com/a8793ce9-86dc-4d7e-aa70-361a3c5a5150/api']
  },
  applicationInsights: {
    connectionString: '',
    enableCorsCorrelation: true
  },
  defaultLanguage: 'nl'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// Import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
