import {ToFormControls} from "../base/form-group.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as uuid from "uuid";

export type RoleForm = ToFormControls<RoleModel>;

export class RoleModel {
  id: string | null = null;
  roleCode: number | null = null;
  name: string | null = null;
  isActive: boolean | null = true;
}

export function createDefaultRoleForm() {
  return new FormGroup<RoleForm>(<RoleForm>{
    id: new FormControl(uuid.v4(), {validators: [Validators.required]}),
    roleCode: new FormControl(null, {validators: [Validators.required]}),
    name: new FormControl('', {validators: [Validators.required]}),
    isActive: new FormControl(true),
  });
}
