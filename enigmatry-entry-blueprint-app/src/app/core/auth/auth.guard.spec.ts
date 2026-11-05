import { TestBed } from '@angular/core/testing';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { EntryPermissionService } from '@enigmatry/entry-components/permissions';
import { CurrentUserService } from '@services/current-user.service';
import { Observable } from 'rxjs';
import { authGuard } from './auth.guard';
import { userCases } from './auth.guard.spec.data';
import { AuthService } from './auth.service';
import { UserProfile } from './user-profile';

const loginRedirectMock = vi.fn().mockReturnValue(Promise.resolve());
const logoutMock = vi.fn();

const mockServicesWith = (user: UserProfile | null | undefined) => {
    TestBed.overrideProvider(AuthService, { useValue: {
        isAuthenticated: () => user !== undefined,
        logout: logoutMock,
        loginRedirect: loginRedirectMock
    } });
    TestBed.overrideProvider(CurrentUserService, { useValue: {
        loadUser: () => vi.fn(),
        currentUser: () => user
    } });
};

const act = async(route: ActivatedRouteSnapshot) =>
    await authGuard(route, {} as unknown as RouterStateSnapshot) as Observable<boolean>;

beforeEach(() => {
    logoutMock.mockClear();
    loginRedirectMock.mockClear();
    TestBed.configureTestingModule({
        providers: [
            AuthService,
            CurrentUserService,
            { provide: EntryPermissionService, useValue: { hasPermissions: () => false } }
        ]
    });
});

describe.each(userCases)('Testing authentication guard...', userCase => {
    it(userCase.description, async() => {
        mockServicesWith(userCase.user);
        await TestBed.runInInjectionContext(async() => {
            const result = await act(userCase.permissionsRequired ?
                { data: { permissions: [] } } as unknown as ActivatedRouteSnapshot : { data: {} } as ActivatedRouteSnapshot);

            expect(result).toBe(userCase.allowedAccess);
            expect(loginRedirectMock).toHaveBeenCalledTimes(userCase.redirectedToLogin ? 1 : 0);
            expect(logoutMock).toHaveBeenCalledTimes(userCase.logout ? 1 : 0);
        });
    });
});