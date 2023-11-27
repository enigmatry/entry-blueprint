import { ElementRef, Injectable } from '@angular/core';
import { Observable, Subject, fromEvent } from 'rxjs';
import { distinctUntilKeyChanged } from 'rxjs/operators';
import { ScreenSizeFactory } from '../models/screen-sizes/factory';
import { Size } from '../models/screen-sizes/size';

@Injectable({
    providedIn: 'root'
})
export class SizeService {
    lastKnownSize: Size;
    onResize$: Observable<Size>;

    private readonly sizeHolderSelector = '.root-container';
    private readonly holderPseudoSelector = '::before';
    private readonly holderPropertyName: string = 'content';

    private resizeSubject = new Subject<Size>();

    constructor() {
        this.onResize$ = this.resizeSubject.asObservable()
            .pipe(distinctUntilKeyChanged('name'));
    }

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

        this.lastKnownSize = ScreenSizeFactory.createFrom(size);
        this.resizeSubject.next(this.lastKnownSize);
    };
}
