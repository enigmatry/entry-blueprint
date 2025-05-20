import { ElementRef, Injectable, signal } from '@angular/core';
import { ScreenSizeFactory } from '@app/models/screen-sizes/factory';
import { Size } from '@app/models/screen-sizes/size';
import { fromEvent } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class SizeService {
    lastKnownSize = signal<Size | undefined>(undefined);

    private readonly sizeHolderSelector = '.root-container';
    private readonly holderPseudoSelector = '::before';
    private readonly holderPropertyName: string = 'content';

    readonly startTrackingResizeOf = (element: ElementRef) => {
        fromEvent(window, 'resize')
            .subscribe(() => this.onResize(element));
        this.onResize(element);
    };

    private readonly onResize = (element: ElementRef) => {
        const nativeElement = element.nativeElement;
        const elementFound = [nativeElement, ...nativeElement.querySelectorAll(this.sizeHolderSelector)]
            .filter(current => current.matches(this.sizeHolderSelector))[0];
        const placeholderStyle = window.getComputedStyle(elementFound, this.holderPseudoSelector);
        const value = placeholderStyle.getPropertyValue(this.holderPropertyName);
        const size = value.replace(/"/gu, '');

        this.lastKnownSize.set(ScreenSizeFactory.createFrom(size));
    };
}
