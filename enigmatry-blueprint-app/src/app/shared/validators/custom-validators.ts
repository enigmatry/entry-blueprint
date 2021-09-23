/* eslint-disable no-secrets/no-secrets */
/* eslint-disable id-length */
/* eslint-disable no-confusing-arrow */
import { FormControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { ValidatorsService as CustomValidatorsService } from './validators.service';

export { ValidatorsService as CustomValidatorsService } from './validators.service';

const productCodeIsUniqueValidator = (control: FormControl, service: CustomValidatorsService)
    : Promise<{ productCodeIsUnique: boolean } | null> =>
    control.dirty
        ? service.isCodeUnique(control.value)
            .pipe(
                map(response => response?.isUnique ? null : { productCodeIsUnique: true })
            )
            .toPromise()
        : Promise.resolve(null);

const productNameIsUniqueValidator = (control: FormControl, service: CustomValidatorsService)
    : Promise<{ productNameIsUnique: boolean } | null> =>
    control.dirty
        ? service.isNameUnique(control.value)
            .pipe(
                map(response => response?.isUnique ? null : { productNameIsUnique: true })
            )
            .toPromise()
        : Promise.resolve(null);

const isLinkValidator = (control: FormControl)
    : Promise<{ isLink: boolean } | null> =>
    control.dirty && control.value
        // eslint-disable-next-line max-len
        ? Promise.resolve(/https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)/u.test(control.value)
            ? null
            : { isLink: true })
        : Promise.resolve(null);

export const customValidatorsFactory = (service: CustomValidatorsService) => {
    return {
        validationMessages: [
            { name: 'productCodeIsUnique', message: $localize`:@@validators.productCodeIsUnique:Code is not unique` },
            { name: 'productNameIsUnique', message: $localize`:@@validators.productNameIsUnique:Name is not unique` },
            { name: 'isLink', message: $localize`:@@validators.isLink:Not valid url address format` }
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
                name: 'isLink',
                validation: (control: FormControl) => isLinkValidator(control)
            }
        ]
    };
};
