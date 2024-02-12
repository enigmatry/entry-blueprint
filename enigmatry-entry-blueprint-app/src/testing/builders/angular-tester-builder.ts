import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { provideLocationMocks } from '@angular/common/testing';
import { TestBed } from '@angular/core/testing';
import { Routes, provideRouter } from '@angular/router';
import { RouterTestingHarness } from '@angular/router/testing';
import { AngularTester } from 'src/testing/builders/angular-tester';

export { AngularTester } from 'src/testing/builders/angular-tester';
export class AngularTesterBuilder {
    private harness: RouterTestingHarness;
    private imports: unknown[] = [];
    private providers: unknown[] = [provideLocationMocks()];
    private testingInterceptors = false;

    readonly withImports = (...imports: unknown[]) => {
        this.imports = imports;
        return this;
    };

    readonly withProviders = (...providers: unknown[]) => {
        this.providers.push(providers);
        return this;
    };

    readonly withInterceptors = () => {
        this.testingInterceptors = true;
        return this;
    };

    readonly withRouter = (...routes: Routes) => {
        this.providers.push(provideRouter(routes));
        return this;
    };

    readonly build = async() => {
        await TestBed.configureTestingModule({
            imports: this.imports,
            providers: this.providers
        }).compileComponents();

        this.harness = await RouterTestingHarness.create();
        await this.harness.navigateByUrl('/');

        if (this.testingInterceptors) {
            TestBed.inject(HTTP_INTERCEPTORS);
        }

        return new AngularTester(this.testingInterceptors);
    };
}