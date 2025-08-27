import { UserProfile } from "./user-profile";

export const userCases = [
    {
        description: 'it should redirect to login page if user is not authenticated',
        user: undefined,
        allowedAccess: false,
        redirectedToLogin: true,
        logout: false
    },
    {
        description: 'it should logout user if we cannot retrieve its profile',
        user: null,
        allowedAccess: false,
        redirectedToLogin: false,
        logout: true
    },
    {
        description: 'it should allow user to enter if profile reachable',
        user: new UserProfile({ fullName: 'John Doe' }),
        allowedAccess: true,
        redirectedToLogin: false,
        logout: false
    }
];