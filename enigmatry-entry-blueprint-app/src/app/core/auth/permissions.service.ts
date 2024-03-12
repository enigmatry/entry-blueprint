import { Injectable } from '@angular/core';
import { PermissionId } from '@api';
import { EntryPermissionService } from '@enigmatry/entry-components/permissions';
import { CurrentUserService } from '@services/current-user.service';

@Injectable({
  providedIn: 'root'
})
export class PermissionService implements EntryPermissionService<PermissionId> {
  constructor(private currentUserService: CurrentUserService) {}

  hasPermissions(permissions: PermissionId[]): boolean {
    const currentUser = this.currentUserService.currentUser;
    if (!currentUser) {
      return false;
    }
    return permissions.some(permission => currentUser.hasPermission(permission));
  }
}