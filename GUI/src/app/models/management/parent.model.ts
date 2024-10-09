import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ToFormControls } from "../base/form-group.model";
import * as uuid from 'uuid';

export type ParentForm = ToFormControls<ParentModel>;

export class ParentModel {
  id: string | null = null;
  firstName: string | null = null;
  lastName: string | null = null;
  phone: string | null = null;
  email: string | null = null;
  note: string | null = null;
}

export function createDefaultParentForm() {
  return new FormGroup<ParentForm>(<ParentForm>{
    id: new FormControl(uuid.v4(), { validators: [Validators.required] }),
    firstName: new FormControl('', { validators: [Validators.required] }),
    lastName: new FormControl('', { validators: [Validators.required] }),
    phone: new FormControl('', { validators: [Validators.required] }),
    email: new FormControl('', { validators: [Validators.required] }),
    note: new FormControl(''),
  });
}
