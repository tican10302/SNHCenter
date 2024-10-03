import {Permission} from "./permission";
import {Menu} from "./menu";
import {Account} from "./account";
import {GroupPermission} from "./groupPermission";


export interface User {
  account: Account;
  permission: Permission[];
  menu: Menu[];
  groupPermission: GroupPermission[];
  token: string;
}
