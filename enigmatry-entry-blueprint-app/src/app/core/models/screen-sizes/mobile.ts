import { Size } from './size';

export class MobileSize extends Size {
    readonly supportsSideMenu = true;
    readonly name = 'Mobile';
}