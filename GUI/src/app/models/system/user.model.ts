import {PermissionModel} from "./permission.model";
import {MenuModel} from "./menu.model";
import {AccountModel} from "./account.model";
import {GroupPermissionModel} from "./group-permission.model";


export interface UserModel {
  account: AccountModel;
  permission: PermissionModel[];
  menu: MenuModel[];
  groupPermission: GroupPermissionModel[];
  token: string;
}
