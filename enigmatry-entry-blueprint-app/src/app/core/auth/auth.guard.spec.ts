import { TestBed } from '@angular/core/testing';
import { RouterStateSnapshot } from '@angular/router';
import { Observable, lastValueFrom, of } from 'rxjs';
import { mockClass } from 'src/testing/mocks/class-mocker';
import { CurrentUserService } from '../services/current-user.service';
import { authGuard } from './auth.guard';
import { userCases } from './auth.guard.spec.data';
import { AuthService } from './auth.service';
import { UserProfile } from './user-profile';

jest.mock('./auth.service');
jest.mock('../services/current-user.service');
const loginRedirectMock = jest.fn().mockReturnValue(Promise.resolve());
const logoutMock = jest.fn();

const mockServicesWith = (user: UserProfile | null | undefined) => {
    mockClass(AuthService, () => Object({
        isAuthenticated: () => user !== undefined,
        logout: logoutMock,
        loginRedirect: loginRedirectMock
    }));
    mockClass(CurrentUserService, () => Object({
        loadUser: () => of(user)
    }));
};

const act = async() =>
    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
    lastValueFrom(await authGuard(undefined!, {} as unknown as RouterStateSnapshot) as Observable<boolean>);

beforeEach(() => {
    logoutMock.mockClear();
    loginRedirectMock.mockClear();
    TestBed.configureTestingModule({ providers: [AuthService, CurrentUserService] });
});

describe.each(userCases)('Testing authentication guard...', userCase => {
    it(userCase.description, async() => {
        await TestBed.runInInjectionContext(async() => {
            mockServicesWith(userCase.user);

            const result = await act();

            expect(result).toBe(userCase.allowedAccess);
            expect(loginRedirectMock).toBeCalledTimes(userCase.redirectedToLogin ? 1 : 0);
            expect(logoutMock).toBeCalledTimes(userCase.logout ? 1 : 0);
        });
    });
});