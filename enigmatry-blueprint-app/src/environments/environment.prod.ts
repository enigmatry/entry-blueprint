/* eslint-disable no-secrets/no-secrets */
export const environment = {
  production: true,
  apiUrl: '__apiUrl__',
  azureAdB2C: {
    clientId: '__azureAdB2cClientId__',
    instance: '__azureAdB2cInstance__',
    domain: '__azureAdB2cDomain__',
    signUpSignInPolicyId: '__azureAdB2cSignUpSignInPolicyId__',
    scopes: ['__azureAdB2cApiScope__']
  }
};
