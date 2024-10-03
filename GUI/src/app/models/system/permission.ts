export interface Permission {
  role: string | null;
  roleId: string | null;
  controllerName: string;
  isView: boolean;
  isAdd: boolean;
  isEdit: boolean;
  isDelete: boolean;
  isApprove: boolean;
  isStatistic: boolean;
}
