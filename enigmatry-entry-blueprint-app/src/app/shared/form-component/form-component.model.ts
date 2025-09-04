import { inject, effect } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { IFieldExpressionDictionary, IFieldPropertyExpressionDictionary }
  from '@enigmatry/entry-form';
import { FormAccessMode } from './form-access-mode.enum';

export abstract class FormComponent<TCommandModel, TDetailsModel> {
  model: TCommandModel = {} as TCommandModel;
  initialModelData: TCommandModel = {} as TCommandModel;
  formMode = FormAccessMode.edit;
  fieldsHideExpressions: IFieldExpressionDictionary<TDetailsModel> = {};
  fieldsDisableExpressions: IFieldExpressionDictionary<TDetailsModel> = {};
  fieldsRequiredExpressions: IFieldExpressionDictionary<TDetailsModel> = {};
  fieldsPropertyExpressions: IFieldPropertyExpressionDictionary<TDetailsModel> = {};
  protected router: Router = inject(Router);
  protected activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  private readonly routeData = toSignal(this.activatedRoute.data);

  constructor() {
    this.formMode = this.getFormAccessMode(this.router.url);
    effect(() => {
      const data = this.routeData();
      if (this.isEdit()) {
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        this.initialModelData = this.toCommand(data!['response']);
        this.resetModel();
      }
    });
  }

  abstract toCommand: (response: TDetailsModel) => TCommandModel;

  resetModel(): void {
    this.model = { ...this.initialModelData };
  }

  goBack = () => this.router.navigate([this.isCreate() ? '../' : '../../'], { relativeTo: this.activatedRoute });

  isCreate = (): boolean => this.formMode === FormAccessMode.create;

  isEdit = (): boolean => this.formMode === FormAccessMode.edit;

  private readonly getFormAccessMode = (url: string): FormAccessMode =>
    url.endsWith(`/create`)
      ? FormAccessMode.create
      : FormAccessMode.edit;
}
