import { Size } from './size';

export class DesktopSize extends Size {
    readonly supportsSideMenu = false;
    readonly name = 'Desktop';
}