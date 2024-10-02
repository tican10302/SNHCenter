import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ToFormControls} from "../base/ModelFormGroup";

export class LoginModel {
  userName: string | null = null;
  password: string| null = null;
}

export type LoginForm = ToFormControls<LoginModel>;

export function createDefaultLoginForm() {
  return new FormGroup<LoginForm>(<LoginForm>{
    userName: new FormControl('', {validators: [Validators.required]}),
    password: new FormControl('', {validators: [Validators.required]}),
  });
}
