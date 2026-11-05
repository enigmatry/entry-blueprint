import { Location } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpTestingController, TestRequest } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { environment } from '@env';
import { firstValueFrom } from 'rxjs';

export class AngularTester {
    private readonly testEndpoint = `${environment.apiUrl}/test`;
    private httpMock: HttpTestingController;
    private httpClient: HttpClient;
    location: Location;
    router: Router;

    constructor() {
        this.httpMock = TestBed.inject(HttpTestingController);
        this.httpClient = TestBed.inject(HttpClient);
        this.router = TestBed.inject(Router);
        this.location = TestBed.inject(Location);
        this.router.initialNavigation();
    }

    readonly requestFailure = async(statusCode: number | undefined, statusText: string,
        // eslint-disable-next-line @typescript-eslint/no-empty-function
        expectedResults: (error: HttpErrorResponse) => void = () => { },
        skipCheck: boolean = false) => {
        const response = firstValueFrom(this.httpClient.get<never>(this.testEndpoint));

        if (skipCheck) {
            this.httpMock.expectNone(this.testEndpoint);
        } else {
            const request = this.httpMock.expectOne(this.testEndpoint);
            request.flush({}, { status: statusCode, statusText });
        }

        await expect(response, `should have failed with the ${statusCode} error`).rejects.toBeDefined();
        const error = await response.catch((failure: HttpErrorResponse) => failure);
        expect(error.status).toEqual(skipCheck ? undefined : statusCode);
        expectedResults(error);
    };

    readonly requestSuccess = async(expectedResults: (result: TestRequest) => void,
        url: string | undefined = this.testEndpoint) => {
        const response = firstValueFrom(this.httpClient.get<object>(url));
        const request = this.httpMock.expectOne(url);
        request.flush({});
        await response;
        expectedResults(request);
    };
}
