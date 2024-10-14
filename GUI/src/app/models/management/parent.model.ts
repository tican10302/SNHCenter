import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ToFormControls } from "../base/form-group.model";
import * as uuid from 'uuid';
import { GetListRequestModel } from "../base/get-list-request.model";


export type ParentForm = ToFormControls<ParentModel>;

export class ParentModel {
  id: string | null = null;
  firstName: string | null = null;
  lastName: string | null = null;
  phone: string | null = null;
  email: string | null = null;
  provinceId: string | null = null;
  districtId: string | null = null;
  wardId: string | null = null;
  note: string | null = null;
}
export class GetListParentRequestModel extends GetListRequestModel {
  provinceId: string | null = null;
  districtId: string | null = null;
  wardId: string | null = null;

}

export function createDefaultParentForm() {
  return new FormGroup<ParentForm>(<ParentForm>{
    id: new FormControl(uuid.v4(), { validators: [Validators.required] }),
    firstName: new FormControl('', { validators: [Validators.required] }),
    lastName: new FormControl('', { validators: [Validators.required] }),
    phone: new FormControl('', { validators: [Validators.required] }),
    email: new FormControl('', { validators: [Validators.required] }),
    provinceId: new FormControl(null, { validators: [Validators.required] }),
    districtId: new FormControl(null, { validators: [Validators.required] }),
    wardId: new FormControl(null, { validators: [Validators.required] }),
    note: new FormControl(''),
  });
}
