import { HTTP_INTERCEPTORS, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { HttpClientTestingModule, TestRequest } from '@angular/common/http/testing';
import { AngularTesterBuilder, AngularTester } from '@test/builders/angular-tester-builder';
import { of, throwError } from 'rxjs';
import { AuthInterceptor } from './auth.interceptor';
import { AuthService } from './auth.service';

const expectedTokenValue = 'token123';
const expectedErrorMessage = 'fatal error';
let throwCustomError = false;
const loginMock = jest.fn().mockReturnValue(Promise.resolve());
const tokenMock = jest.fn(() =>
  throwCustomError ? throwError(() => new Error(expectedErrorMessage)) : of(expectedTokenValue));

jest.mock('./auth.service');
const mockAuthService = (): AuthService => ({
  getAccessToken: tokenMock,
  loginRedirect: loginMock
} as unknown as AuthService);

let tester: AngularTester;

beforeEach(async() => {
  tokenMock.mockClear();
  tester = await new AngularTesterBuilder()
    .withProviders([{
      provide: AuthService,
      useFactory: mockAuthService
    },
    {
      provide: HTTP_INTERCEPTORS,
      deps: [AuthService],
      useClass: AuthInterceptor,
      multi: true
    }
    ])
    .withImports(HttpClientTestingModule)
    .withInterceptors()
    .build();
});

describe('Testing auth interceptor...', () => {
  it(`should do nothing if it's non-API request`, (done: jest.DoneCallback) => {
    tester.requestSuccess(done, () => {
      expect(tokenMock).not.toHaveBeenCalled();
    }, `/test`);
  });

  it(`should add bearer token in headers to API requests`, (done: jest.DoneCallback) => {
    tester.requestSuccess(done, (result: TestRequest) => {
      expect(tokenMock).toHaveBeenCalledTimes(1);
      expect(result.request.headers.get('Authorization')).toEqual(`Bearer ${expectedTokenValue}`);
    });
  });

  it(`should throw in case of an unknown exception`, (done: jest.DoneCallback) => {
    throwCustomError = true;
    tester.requestFailure(undefined, '', done, (error: HttpErrorResponse) => {
      expect(tokenMock).toHaveBeenCalledTimes(1);
      expect(error.message).toBe(expectedErrorMessage);
    }, true);
  });

  it(`should throw in case when any other status but 401`, (done: jest.DoneCallback) => {
    throwCustomError = false;
    tester.requestFailure(HttpStatusCode.Forbidden, HttpStatusCode.Forbidden.toString(), done, () => {
        expect(tokenMock).toHaveBeenCalledTimes(1);
      });
  });

  it(`should redirect to login screen when response status 401`, (done: jest.DoneCallback) => {
    throwCustomError = false;
    tester.requestFailure(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.toString(), done, () => {
        expect(tokenMock).toHaveBeenCalledTimes(1);
        expect(loginMock).toHaveBeenCalledTimes(1);
      });
  });
});
