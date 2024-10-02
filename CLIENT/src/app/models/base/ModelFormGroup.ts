import {FormControl, FormGroup} from "@angular/forms";

export type ModelFormGroup<T> = FormGroup<{
  [K in keyof T]: FormControl<T[K]>;
}>;

export type ToFormControls<T> = {
  [K in keyof T]: FormControl<T[K] | null>;
};

export function createFormGroup<T extends object>(model: T): FormGroup<ToFormControls<T>> {
  const formGroup: { [K in keyof T]: FormControl<T[K] | null> } = {} as any;

  Object.keys(model).forEach(key => {
    formGroup[key as keyof T] = new FormControl(model[key as keyof T] ?? null);
  });

  return new FormGroup(formGroup);
}
