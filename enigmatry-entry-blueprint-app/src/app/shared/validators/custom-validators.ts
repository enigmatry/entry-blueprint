import { UntypedFormControl } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { ValidatorsService as CustomValidatorsService } from './validators.service';

export { ValidatorsService as CustomValidatorsService } from './validators.service';

const productCodeIsUniqueValidator = async(control: UntypedFormControl, service: CustomValidatorsService)
    : Promise<{ productCodeIsUnique: boolean } | null> => {
    if (control.dirty) {
        const productId = control?.parent?.get('id')?.value;
        const response = await firstValueFrom(service.isCodeUnique(productId, control.value));
        return response?.isUnique ? null : { productCodeIsUnique: true };
    }
    return Promise.resolve(null);
};

const productNameIsUniqueValidator = async(control: UntypedFormControl, service: CustomValidatorsService)
    : Promise<{ productNameIsUnique: boolean } | null> => {
    if (control.dirty) {
        const productId = control?.parent?.get('id')?.value;
        const response = await firstValueFrom(service.isNameUnique(productId, control.value));
        return response?.isUnique ? null : { productNameIsUnique: true };
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
