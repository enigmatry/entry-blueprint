import { ActivatedRoute, Router } from '@angular/router';
import { FormAccessMode } from './form-access-mode.enum';
import { IFieldExpressionDictionary } from '@enigmatry/angular-building-blocks/form';

export abstract class FormComponent<TCommandModel, TDetailsModel> {
    model: TCommandModel = {} as TCommandModel;
    initialModelData: TCommandModel = {} as TCommandModel;
    formMode = FormAccessMode.edit;
    fieldsHideExpressions: IFieldExpressionDictionary<TDetailsModel> = {};
    fieldsDisableExpressions: IFieldExpressionDictionary<TDetailsModel> = {};
    fieldsRequiredExpressions: IFieldExpressionDictionary<TDetailsModel> = {};
    cancelText = $localize`:@@common.cancel:Cancel`;
    saveText = $localize`:@@common.save:Save`;

    constructor(protected router: Router, protected activatedRoute: ActivatedRoute) {
        this.formMode = this.getFormAccessMode(router.url);

        if (this.isEdit()) {
            this.activatedRoute.data.subscribe(data => {
                this.initialModelData = this.toCommand(data.response);
                this.resetModel();
            });
        }
    }

    abstract toCommand(response: TDetailsModel): TCommandModel;

    resetModel(): void {
      this.model = { ...this.initialModelData };
    }

    goBack = () => this.router.navigate([this.isCreate() ? '../' : '../../'], { relativeTo: this.activatedRoute });

    isCreate = (): boolean => this.formMode === FormAccessMode.create;

    isEdit = (): boolean => this.formMode === FormAccessMode.edit;

    // eslint-disable-next-line no-confusing-arrow
    private readonly getFormAccessMode = (url: string): FormAccessMode =>
        url.endsWith(`/create`)
            ? FormAccessMode.create
            : FormAccessMode.edit;
}
