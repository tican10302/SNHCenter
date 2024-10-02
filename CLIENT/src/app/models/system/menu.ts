export interface Menu {
  controllerName: string | null;
  controller: string | null;
  action: string | null;
  name: string | null;
  icon: string | null;
  groupPermissionId: string;
  groupName: string | null;
  groupSort: number;
  hasView: boolean;
  hasAdd: boolean;
  hasEdit: boolean;
  hasDelete: boolean;
  hasApprove: boolean;
  hasStatistic: boolean;
  isShowMenu: boolean;
  sort: number;
}
