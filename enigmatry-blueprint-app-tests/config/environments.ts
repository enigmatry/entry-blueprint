export const environments: IEnvInfo[] = [
  { name: 'local', url: 'http://localhost:4200' },
  { name: 'devci', url: 'https://enigmatry-blueprint-test.azurewebsites.net' },
  { name: 'uat1', url: 'https://enigmatry-blueprint-test.azurewebsites.net' },
  { name: 'prod', url: 'https://enigmatry-blueprint-test.azurewebsites.net' },
];

const envNames: string[] = environments.map((e) => e.name);

export function env(name: TargetEnvironment | undefined): IEnvInfo | undefined {
  if (name === undefined) {
    // tslint:disable-next-line:no-console
    console.warn(`Environment name is undefined. Available environments are: ${envNames}.
      (You can optionally add to the testcafe command-line the option: --env=${envNames[0]})`);
    return undefined;
  }
  const foundEnvironment = environments.filter((e) => e.name === name)[0];
  if (foundEnvironment) {
    return foundEnvironment;
  }

  // tslint:disable-next-line:no-console
  console.warn(`Environment "${name}" is not found. Available environments are: ${envNames}`);
  return undefined;
}

export interface IEnvInfo {
  name: TargetEnvironment;
  url: string;
}
export type TargetEnvironment = 'any' | 'local' | 'devci' | 'uat1' | 'prod';
