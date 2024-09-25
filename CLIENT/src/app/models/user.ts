import {Permission} from "./permission";
import {Menu} from "./menu";
import {Account} from "./account";
import {groupPermission} from "./groupPermission";


export interface User {
  account: Account | null;
  permission: Permission[] | null;
  menu: Menu[] | null;
  groupPermission: groupPermission[] | null;
  userName: string;
  token: string;
}
