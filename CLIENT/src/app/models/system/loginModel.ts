import {FormControl, FormGroup, Validators} from "@angular/forms";

type ToFormControls<T> = {
  [K in keyof T]: FormControl<T[K]>;
};

export interface LoginModel {
  userName: string;
  password: string;
}

export type LoginForm = ToFormControls<LoginModel>;

export function createDefaultLoginForm() {
  return new FormGroup<LoginForm>(<LoginForm>{
    userName: new FormControl('', {validators: [Validators.required]}),
    password: new FormControl('', {validators: [Validators.required]}),
  });
}
