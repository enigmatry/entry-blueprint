import { UntypedFormControl } from '@angular/forms';
import { Observable, firstValueFrom } from 'rxjs';
import { ValidatorsService as CustomValidatorsService } from './validators.service';

export { ValidatorsService as CustomValidatorsService } from './validators.service';

const productCodeIsUniqueValidator = async(control: UntypedFormControl, service: CustomValidatorsService)
    : Promise<{ productCodeIsUnique: boolean } | null> =>
    // eslint-disable-next-line @typescript-eslint/return-await
    await validate(control, service.isCodeUnique, { productCodeIsUnique: true });

const productNameIsUniqueValidator = async(control: UntypedFormControl, service: CustomValidatorsService)
    : Promise<{ productNameIsUnique: boolean } | null> =>
    // eslint-disable-next-line @typescript-eslint/return-await
    await validate(control, service.isNameUnique, { productNameIsUnique: true });

const validate = async<T>(control: UntypedFormControl,
    callback: (productId: string, value: string) => Observable<{ isUnique?: boolean | undefined }>,
    expectedResult: T) => {
    if (control.dirty) {
        const productId = control.parent?.get('id')?.value;
        const response = await firstValueFrom(callback(productId, control.value));
        return response?.isUnique ? null : expectedResult;
    }
    return Promise.resolve(null);
};

export const customValidatorsFactory = (service: CustomValidatorsService) => {
    return {
        validationMessages: [
            { name: 'productCodeIsUnique', message: $localize`:@@validators.productCodeIsUnique:Code is not unique` },
            // eslint-disable-next-line no-secrets/no-secrets
            { name: 'productNameIsUnique', message: $localize`:@@validators.productNameIsUnique:Name is not unique` }
        ],
        validators: [
            {
                name: 'productCodeIsUnique',
                validation: (control: UntypedFormControl) => productCodeIsUniqueValidator(control, service)
            },
            {
                // eslint-disable-next-line no-secrets/no-secrets
                name: 'productNameIsUnique',
                validation: (control: UntypedFormControl) => productNameIsUniqueValidator(control, service)
            }
        ]
    };
};
