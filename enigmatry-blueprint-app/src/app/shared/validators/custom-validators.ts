/* eslint-disable max-len */
/* eslint-disable no-secrets/no-secrets */
/* eslint-disable id-length */
import { UntypedFormControl } from '@angular/forms';
import { ValidatorsService as CustomValidatorsService } from './validators.service';

export { ValidatorsService as CustomValidatorsService } from './validators.service';

const productCodeIsUniqueValidator = async(control: UntypedFormControl, service: CustomValidatorsService)
    : Promise<{ productCodeIsUnique: boolean } | null> => {
    if (control.dirty) {
        const productId = control?.parent?.get('id')?.value;
        const response = await service.isCodeUnique(productId, control.value).toPromise();
        return response.isUnique ? null : { productCodeIsUnique: true };
    }
    return Promise.resolve(null);
};

const productNameIsUniqueValidator = async(control: UntypedFormControl, service: CustomValidatorsService)
    : Promise<{ productNameIsUnique: boolean } | null> => {
    if (control.dirty) {
        const productId = control?.parent?.get('id')?.value;
        const response = await service.isNameUnique(productId, control.value).toPromise();
        return response.isUnique ? null : { productNameIsUnique: true };
    }
    return Promise.resolve(null);
};

export const customValidatorsFactory = (service: CustomValidatorsService) => {
    return {
        validationMessages: [
            { name: 'productCodeIsUnique', message: $localize`:@@validators.productCodeIsUnique:Code is not unique` },
            { name: 'productNameIsUnique', message: $localize`:@@validators.productNameIsUnique:Name is not unique` }
        ],
        validators: [
            {
                name: 'productCodeIsUnique',
                validation: (control: UntypedFormControl) => productCodeIsUniqueValidator(control, service)
            },
            {
                name: 'productNameIsUnique',
                validation: (control: UntypedFormControl) => productNameIsUniqueValidator(control, service)
            }
        ]
    };
};
