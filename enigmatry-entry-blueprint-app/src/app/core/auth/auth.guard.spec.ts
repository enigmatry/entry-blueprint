import { TestBed } from '@angular/core/testing';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { CurrentUserService } from '@services/current-user.service';
import { mockClass } from '@test/mocks/class-mocker';
import { Observable } from 'rxjs';
import { authGuard } from './auth.guard';
import { userCases } from './auth.guard.spec.data';
import { AuthService } from './auth.service';
import { UserProfile } from './user-profile';

jest.mock('./auth.service');
jest.mock('../services/current-user.service');
jest.mock('@enigmatry/entry-components', () => ({
    __esModule: true,
    entryPermissionGuard: jest.fn(() => false)
}));
const loginRedirectMock = jest.fn().mockReturnValue(Promise.resolve());
const logoutMock = jest.fn();

const mockServicesWith = (user: UserProfile | null | undefined) => {
    mockClass(AuthService, () => Object({
        isAuthenticated: () => user !== undefined,
        logout: logoutMock,
        loginRedirect: loginRedirectMock
    }));
    mockClass(CurrentUserService, () => Object({
        loadUser: () => jest.fn(),
        currentUser: () => user
    }));
};

const act = async(route: ActivatedRouteSnapshot) =>
    await authGuard(route, {} as unknown as RouterStateSnapshot) as Observable<boolean>;

beforeEach(() => {
    logoutMock.mockClear();
    loginRedirectMock.mockClear();
    TestBed.configureTestingModule({ providers: [AuthService, CurrentUserService] });
});

describe.each(userCases)('Testing authentication guard...', userCase => {
    it(userCase.description, async() => {
        await TestBed.runInInjectionContext(async() => {
            mockServicesWith(userCase.user);

            const result = await act(userCase.permissionsRequired ?
                { data: { permissions: [] } } as unknown as ActivatedRouteSnapshot : { data: {} } as ActivatedRouteSnapshot);

            expect(result).toBe(userCase.allowedAccess);
            expect(loginRedirectMock).toHaveBeenCalledTimes(userCase.redirectedToLogin ? 1 : 0);
            expect(logoutMock).toHaveBeenCalledTimes(userCase.logout ? 1 : 0);
        });
    });
});