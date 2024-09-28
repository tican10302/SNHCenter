export interface ModelBase {
  id: string;
  createdAt: Date | null;
  createdBy: string | null;
  updatedAt: Date | null;
  updatedBy: string | null;
  isEdit: boolean | false;
  isActived: boolean | true;
  sort: number | null;
}
