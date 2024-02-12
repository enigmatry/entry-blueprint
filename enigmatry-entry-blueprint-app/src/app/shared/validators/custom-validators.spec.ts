import { UntypedFormControl } from '@angular/forms';
import { of } from 'rxjs';
import { CustomValidatorsService, customValidatorsFactory } from './custom-validators';

const defaultControl = (dirty = true, returnNull = false) => ({
    dirty,
    value: returnNull,
    parent: {
        get: (key: string) => {
            return {
                value: key
            };
        }
    }
} as unknown as UntypedFormControl);
const getService = (withUniqueCode: boolean, withUniqueName: boolean) => ({
    isCodeUnique: (_: string, returnNull: boolean) => of(returnNull ? null : { isUnique: withUniqueCode }),
    isNameUnique: (_: string, returnNull: boolean) => of(returnNull ? null : { isUnique: withUniqueName })
} as unknown as CustomValidatorsService);

const act = async(propertyName: string, control: UntypedFormControl,
    withUniqueCode: boolean = true, withUniqueName: boolean = true) => {
    const service = getService(withUniqueCode, withUniqueName);
    const result = customValidatorsFactory(service);
    // eslint-disable-next-line @typescript-eslint/return-await
    return await result.validators[propertyName === 'code' ? 0 : 1].validation(control);
};

describe.each([
    { propertyName: 'code', expectedResult: { productCodeIsUnique: true } },
    { propertyName: 'name', expectedResult: { productNameIsUnique: true } }]
)('Testing custom validators...', testCase => {
    it(`should return nothing if input control for ${testCase.propertyName} is unchanged`, async() => {
        const result = await act(testCase.propertyName, defaultControl(false));

        expect(result).toBeNull();
    });

    it(`should return null when ${testCase.propertyName} is not unique and parent is null`, async() => {
        const control = defaultControl() as { parent: {} | undefined };
        control.parent = undefined;

        const result = await act(testCase.propertyName, control as UntypedFormControl, false, false);

        expect(result).toStrictEqual(testCase.expectedResult);
    });

    it(`should return null when ${testCase.propertyName} isn't unique and parent is missing property`, async() => {
        const control = defaultControl() as unknown as { parent: { get: () => string | undefined } };
        control.parent.get = () => undefined;

        const result = await act(testCase.propertyName, control as unknown as UntypedFormControl, false, false);

        expect(result).toStrictEqual(testCase.expectedResult);
    });

    it(`should return null when ${testCase.propertyName} uniqueness response is null`, async() => {
        const result = await act(testCase.propertyName, defaultControl(true, true), true, true);

        expect(result).toStrictEqual(testCase.expectedResult);
    });

    it(`should return valid result when ${testCase.propertyName} is unique`, async() => {
        const result = await act(testCase.propertyName, defaultControl(), true, true);

        expect(result).toBeNull();
    });
});
