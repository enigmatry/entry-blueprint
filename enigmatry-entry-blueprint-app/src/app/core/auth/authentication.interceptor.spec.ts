import { HttpErrorResponse, HttpStatusCode, provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideHttpClientTesting, TestRequest } from '@angular/common/http/testing';
import { AngularTester, AngularTesterBuilder } from '@test/builders/angular-tester-builder';
import { of, throwError } from 'rxjs';
import { AuthService } from './auth.service';
import { authenticationInterceptor } from './authentication.interceptor';

const expectedTokenValue = 'token123';
const expectedErrorMessage = 'fatal error';
let throwCustomError = false;
const loginMock = vi.fn().mockReturnValue(Promise.resolve());
const tokenMock = vi.fn(() =>
  throwCustomError ? throwError(() => new Error(expectedErrorMessage)) : of(expectedTokenValue));

const mockAuthService = (): AuthService => ({
  getAccessToken: tokenMock,
  loginRedirect: loginMock
} as unknown as AuthService);

let tester: AngularTester;

beforeEach(async() => {
  throwCustomError = false;
  tokenMock.mockClear();
  loginMock.mockClear();
  tester = await new AngularTesterBuilder()
    .withProviders([
      provideHttpClient(withInterceptors([authenticationInterceptor])),
      provideHttpClientTesting(),
      {
        provide: AuthService,
        useFactory: mockAuthService
      }])
    .build();
});

describe('Testing auth interceptor...', () => {
  it(`should do nothing if it's non-API request`, async() => {
    await tester.requestSuccess(() => {
      expect(tokenMock).not.toHaveBeenCalled();
    }, `/test`);
  });

  it(`should add bearer token in headers to API requests`, async() => {
    await tester.requestSuccess((result: TestRequest) => {
      expect(tokenMock).toHaveBeenCalledTimes(1);
      expect(result.request.headers.get('Authorization')).toEqual(`Bearer ${expectedTokenValue}`);
    });
  });

  it(`should throw in case of an unknown exception`, async() => {
    throwCustomError = true;
    await tester.requestFailure(undefined, '', (error: HttpErrorResponse) => {
      expect(tokenMock).toHaveBeenCalledTimes(1);
      expect(error.message).toBe(expectedErrorMessage);
    }, true);
  });

  it(`should throw in case when any other status but 401`, async() => {
    throwCustomError = false;
    await tester.requestFailure(HttpStatusCode.Forbidden, HttpStatusCode.Forbidden.toString(), () => {
      expect(tokenMock).toHaveBeenCalledTimes(1);
    });
  });

  it(`should redirect to login screen when response status 401`, async() => {
    throwCustomError = false;
    await tester.requestFailure(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.toString(), () => {
      expect(tokenMock).toHaveBeenCalledTimes(1);
      expect(loginMock).toHaveBeenCalledTimes(1);
    });
  });
});
