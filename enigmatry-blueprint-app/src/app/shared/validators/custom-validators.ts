/* eslint-disable max-len */
/* eslint-disable no-secrets/no-secrets */
/* eslint-disable id-length */
import { FormControl } from '@angular/forms';
import { ProductType } from 'src/app/api/api-reference';
import { ValidatorsService as CustomValidatorsService } from './validators.service';

export { ValidatorsService as CustomValidatorsService } from './validators.service';

const productCodeIsUniqueValidator = async(control: FormControl, service: CustomValidatorsService)
    : Promise<{ productCodeIsUnique: boolean } | null> => {
    if (control.dirty) {
        const productId = control?.parent?.get('id')?.value;
        const response = await service.isCodeUnique(productId, control.value).toPromise();
        return response.isUnique ? null : { productCodeIsUnique: true };
    }
    return Promise.resolve(null);
};

const productNameIsUniqueValidator = async(control: FormControl, service: CustomValidatorsService)
    : Promise<{ productNameIsUnique: boolean } | null> => {
    if (control.dirty) {
        const productId = control?.parent?.get('id')?.value;
        const response = await service.isNameUnique(productId, control.value).toPromise();
        return response.isUnique ? null : { productNameIsUnique: true };
    }
    return Promise.resolve(null);
};

const productExpiresOnIsRequiredValidator = async(control: FormControl)
    : Promise<{ productExpiresOnIsRequired: boolean } | null> => {
        const typeValue = control?.parent?.get('type')?.value;
        if (typeValue === undefined || typeValue === null) {
            return Promise.resolve(null);
        }

        const productType = typeValue as ProductType;
        if (productType === ProductType.Drink || productType === ProductType.Food) {
            const response = control.value
                ? null
                : { productExpiresOnIsRequired: true };
            return Promise.resolve(response);
        }
        return Promise.resolve(null);
};

export const customValidatorsFactory = (service: CustomValidatorsService) => {
    return {
        validationMessages: [
            { name: 'productCodeIsUnique', message: $localize`:@@validators.productCodeIsUnique:Code is not unique` },
            { name: 'productNameIsUnique', message: $localize`:@@validators.productNameIsUnique:Name is not unique` },
            { name: 'productExpiresOnIsRequired', message: $localize`:@@validators.product-expires-on-required:Required for food & drinks` }
        ],
        validators: [
            {
                name: 'productCodeIsUnique',
                validation: (control: FormControl) => productCodeIsUniqueValidator(control, service)
            },
            {
                name: 'productNameIsUnique',
                validation: (control: FormControl) => productNameIsUniqueValidator(control, service)
            },
            {
                name: 'productExpiresOnIsRequired',
                validation: (control: FormControl) => productExpiresOnIsRequiredValidator(control)
            }
        ]
    };
};
