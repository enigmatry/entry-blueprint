/* eslint-disable newline-per-chained-call */
/* eslint-disable id-length */
/* eslint-disable arrow-parens */
/* eslint-disable @typescript-eslint/no-magic-numbers */
/* eslint-disable arrow-body-style */
/* eslint-disable no-promise-executor-return */
/* eslint-disable max-statements-per-line */
/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable unused-imports/no-unused-vars */
/* eslint-disable promise/avoid-new */
import { FormControl } from '@angular/forms';

export const ProductCodeUniquenessValidator = (control: FormControl): Promise<boolean> =>
    new Promise((resolve) =>
        setTimeout(() =>
            resolve(true),
            1000)
        );

export const PhoneNumberValidator = (control: FormControl): boolean =>
    /^[\\+]?[(]?[0-9]{3}[)]?[-\s\\.]?[0-9]{3}[-\s\\.]?[0-9]{4,6}$/imu.test(control.value);