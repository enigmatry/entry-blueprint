export const environment = {
  production: true,
  appVersion: '__appVersion__',
  apiUrl: '__apiUrl__',
  azureAd: {
    clientId: '__azureAdClientId__',
    authority: '__azureAdAuthority__',
    scopes: ['__azureAdScopes__']
  },
  applicationInsights: {
    connectionString: '__applicationInsightsConnectionString__',
    enableCorsCorrelation: false // When 'true' correlation header is added to CORS requests as well
  },
  defaultLanguage: 'nl'
};
