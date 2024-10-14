import {ToFormControls} from "../base/form-group.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as uuid from "uuid";

export type RolePermissionForm = ToFormControls<RolePermissionModel>;

export class RolePermissionModel {
  id: string | null = null;
  roleId: string | null = null;
  controllerName: string | null = null;
  isView: boolean | null = false;
  isAdd: boolean | null = false;
  isEdit: boolean | null = false;
  isDelete: boolean | null = false;
  isApprove: boolean | null = false;
  isStatistic: boolean | null = false;
  hasView: boolean | null = false;
  hasAdd: boolean | null = false;
  hasEdit: boolean | null = false;
  hasDelete: boolean | null = false;
  hasApprove: boolean | null = false;
  hasStatistic: boolean | null = false;
}

export class GetListRolePermissionRequestModel {
  roleId: string | null = null;
  groupId: string | null = null;
}

export function createDefaultRolePermissionForm() {
  return new FormGroup<RolePermissionForm>(<RolePermissionForm>{
    id: new FormControl(uuid.v4(), {validators: [Validators.required]}),
    roleId: new FormControl('', {validators: [Validators.required]}),
    controllerName: new FormControl('', {validators: [Validators.required]}),
    isView: new FormControl(false),
    isAdd: new FormControl(false),
    isEdit: new FormControl(false),
    isDelete: new FormControl(false),
    isStatistic: new FormControl(false),
    isApprove: new FormControl(false),
    hasView: new FormControl(false),
    hasAdd: new FormControl(false),
    hasEdit: new FormControl(false),
    hasDelete: new FormControl(false),
    hasStatistic: new FormControl(false),
    hasApprove: new FormControl(false),
  });
}
