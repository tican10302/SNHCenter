import {ToFormControls} from "../base/form-group.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as uuid from "uuid";
import {GetListRequestModel} from "../base/get-list-request.model";

export type MenuForm = ToFormControls<MenuModel>;

export class MenuModel {
  id: string | null = null;
  controllerName: string | null = null;
  controller: string | null = null;
  action: string | null = null;
  name: string | null = null;
  icon: string | null = null;
  groupPermissionId: string | null = null;
  groupName: string | null = null;
  groupSort: number = 0;
  hasView: boolean = false;
  hasAdd: boolean = false;
  hasEdit: boolean = false;
  hasDelete: boolean = false;
  hasApprove: boolean = false;
  hasStatistic: boolean = false;
  isShowMenu: boolean = true;
  isActive: boolean = true;
  sort: number = 0;
}

export class GetListMenuRequestModel extends GetListRequestModel {
  groupPermissionId: string | null = null;
}

export function createDefaultMenuForm() {
  return new FormGroup<MenuForm>(<MenuForm>{
    id: new FormControl(uuid.v4(), {validators: [Validators.required]}),
    controllerName: new FormControl('', {validators: [Validators.required]}),
    controller: new FormControl('', {validators: [Validators.required]}),
    action: new FormControl('', {validators: [Validators.required]}),
    name: new FormControl('', {validators: [Validators.required]}),
    icon: new FormControl(''),
    groupPermissionId: new FormControl('', {validators: [Validators.required]}),
    hasView: new FormControl(false),
    hasAdd: new FormControl(false),
    hasEdit: new FormControl(false),
    hasDelete: new FormControl(false),
    hasApprove: new FormControl(false),
    hasStatistic: new FormControl(false),
    isShowMenu: new FormControl(true),
    isActive: new FormControl(true),
    sort: new FormControl(0),
  });
}
