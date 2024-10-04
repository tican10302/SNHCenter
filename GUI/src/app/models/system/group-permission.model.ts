import {ToFormControls} from "../base/form-group.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as uuid from "uuid";

export type GroupPermissionForm = ToFormControls<GroupPermissionModel>;

export class GroupPermissionModel {
  id: string | null = null;
  name: string | null = null;
  icon: string | null = null;
  sort: number | null = null;
  isActive: boolean | null = null;
}

export function createDefaultGroupPermissionForm() {
  return new FormGroup<GroupPermissionForm>(<GroupPermissionForm>{
    id: new FormControl(uuid.v4(), {validators: [Validators.required]}),
    name: new FormControl('', {validators: [Validators.required]}),
    icon: new FormControl(''),
    sort: new FormControl(0),
    isActive: new FormControl(true),
  });
}
