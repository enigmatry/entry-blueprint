import { UserProfile } from './user-profile';

export const userCases = [
    {
        description: 'it should redirect to login page if user is not authenticated',
        user: undefined,
        allowedAccess: false,
        redirectedToLogin: true,
        logout: false,
        permissionsRequired: false
    },
    {
        description: 'it should logout user if we cannot retrieve its profile',
        user: null,
        allowedAccess: false,
        redirectedToLogin: false,
        logout: true,
        permissionsRequired: false
    },
    {
        description: 'it should allow user to pass if no permissions required',
        user: new UserProfile({ fullName: 'John Doe' }),
        allowedAccess: true,
        redirectedToLogin: false,
        logout: false,
        permissionsRequired: false
    },
    {
        description: 'it should pass request to entryPermissionGuard if all else fine and permissions required',
        user: new UserProfile({ fullName: 'John Doe' }),
        allowedAccess: false,
        redirectedToLogin: false,
        logout: false,
        permissionsRequired: true
    }
];