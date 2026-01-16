// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  appVersion: '0.0.0',
  apiUrl: 'https://localhost:7251',
  azureAd: {
    clientId: 'e11274d2-cc0b-489a-8d79-838bb6136363',
    authority: 'https://enigmatryexternaldev.ciamlogin.com',
    scopes: ['api://e11274d2-cc0b-489a-8d79-838bb6136363/api']
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
