import { TestBed } from '@angular/core/testing';
import { PermissionId } from '@api';
import { mockClass } from '@test/mocks/class-mocker';
import { CurrentUserService } from '../services/current-user.service';
import { PermissionService } from './permissions.service';
import { UserProfile } from './user-profile';

jest.mock('../services/current-user.service');
beforeEach(() => {
    TestBed.configureTestingModule({ providers: [CurrentUserService, PermissionService] });
});
const arrangePermissionService = (user: UserProfile | null) => {
    mockClass(CurrentUserService, () => Object({
        currentUser: user
    }));
    return TestBed.inject(PermissionService);
};

describe.each([
    { description: 'should forbid actions if there is no user', user: null, allowed: false },
    {
        description: 'should forbid actions if there is no adequate permission',
        user: UserProfile.fromResponse({ permissions: [PermissionId.ProductsRead, PermissionId.UsersRead] }),
        allowed: false
    },
    {
        description: 'should allow actions if there are minimal permissions',
        user: UserProfile.fromResponse({ permissions: [PermissionId.ProductsRead, PermissionId.ProductsWrite] }),
        allowed: true
    }
])('Testing permissions service...', data => {
    it(data.description, () => {
        const service = arrangePermissionService(data.user);

        const result = service.hasPermissions([PermissionId.ProductsWrite, PermissionId.UsersWrite]);

        expect(result).toBe(data.allowed);
    });
});