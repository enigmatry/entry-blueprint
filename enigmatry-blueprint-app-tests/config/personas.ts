export const personas: IUserInfo[] = [
  { name: 'user 1', login: 'user1@example.com', password: '' },
  { name: 'user 2', login: 'user2@example.com', password: '' },
  { name: 'user 3', login: 'user3@example.com', password: '' },
];

const userLogins: string[] = personas.map((p) => p.login);
export function user(login: Email | undefined, password: string | undefined): IUserInfo | undefined {
  if (login === undefined) {
    // tslint:disable-next-line:no-console
    console.warn(`User login is undefined. Available logins are: ${userLogins}.
    (You can optionally add to the testcafe command-line the option: --user=${userLogins[0]})`);
    return undefined;
  }
  const foundUser = personas.filter((p) => p.login === login)[0];
  if (foundUser) {
    if (password) {
      foundUser.password = password
    }
    return foundUser;
  }

  // tslint:disable-next-line:no-console
  console.warn(`User login "${login}" is not found. Available logins are: ${userLogins}`);
  return undefined;
}
export interface IUserInfo {
  name: string;
  login: Email;
  password?: string;
}

export type Email = 'user1@example.com' | 'user2@example.com' | 'user3@example.com';
