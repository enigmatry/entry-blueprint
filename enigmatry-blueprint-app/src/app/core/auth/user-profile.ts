import { GetUserProfileResponse, IGetUserProfileResponse, PermissionId } from 'src/app/api/api-reference';

export class UserProfile extends GetUserProfileResponse {
  hasPermission(permission: PermissionId): boolean {
    return this.permissions?.includes(permission) ?? false;
  }

  static fromResponse(response: IGetUserProfileResponse | null) {
    return response ? new UserProfile(response) : null;
  }
}
