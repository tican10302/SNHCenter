import {FormArray, FormControl, FormGroup} from "@angular/forms";

export type FormGroupModel<T> = FormGroup<{
  [K in keyof T]: FormControl<T[K]>;
}>;

export type ToFormControls<T> = {
  [K in keyof T]: T[K] extends Array<any>
    ? FormArray
    : FormControl<T[K] | null>;
};

export function createFormGroup<T extends object>(model: T): FormGroup<ToFormControls<T>> {
  const formGroup = {} as any;

  Object.keys(model).forEach(key => {
    const value = model[key as keyof T];

    if (Array.isArray(value)) {
      formGroup[key] = new FormArray(value.map(v => new FormControl(v)));
    } else {
      formGroup[key] = new FormControl(value ?? null);
    }
  });

  return new FormGroup(formGroup);
}
