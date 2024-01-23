import { Location } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpTestingController, TestRequest } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';

export class AngularTester {
    private readonly testEndpoint = '/test';
    private httpMock: HttpTestingController;
    private httpClient: HttpClient;
    location: Location;
    router: Router;

    constructor(private readonly testingInterceptors: boolean) {
        if (this.testingInterceptors) {
            this.httpMock = TestBed.inject(HttpTestingController);
            this.httpClient = TestBed.inject(HttpClient);
        }

        this.router = TestBed.inject(Router);
        this.location = TestBed.inject(Location);
        this.router.initialNavigation();
    }

    readonly requestFailure = (statusCode: number | undefined, statusText: string,
        // eslint-disable-next-line @typescript-eslint/no-empty-function
        done: jest.DoneCallback, expectedResults: (error: HttpErrorResponse) => void = () => { },
        url: string | undefined = this.testEndpoint, skipCheck: boolean = false) => {
        this.httpClient.get<object>(url).subscribe({
            next: () => fail(`should have failed with the ${statusCode} error`),
            error: (error: HttpErrorResponse) => {
                // Expect(error).toEqual({});
                expect(error.status).toEqual(skipCheck ? undefined : statusCode);
                expectedResults(error);
                done();
            },
            complete: () => done()
        });

        if (skipCheck) {
            this.httpMock.expectNone(url);
        } else {
            const request = this.httpMock.expectOne(url);
            request?.flush({}, { status: statusCode, statusText });
        }
    };

    readonly requestSuccess = (done: jest.DoneCallback, expectedResults: (result: any) => void,
        url: string | undefined = this.testEndpoint) => {
        let request: TestRequest | null = null;
            this.httpClient.get<object>(url).subscribe({
            next: () => {
                // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
                expectedResults(request!);
                done();
            },
            error: (_error: HttpErrorResponse) => fail(`should not fail`)
        });
        request = this.httpMock.expectOne(url);
        request.flush({});
    };
}