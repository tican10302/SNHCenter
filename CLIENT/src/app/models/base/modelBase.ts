export class ModelBase {
  id: string | null = null;
  createdAt: Date | null = null;
  createdBy: string | null = null;
  updatedAt: Date | null = null;
  updatedBy: string | null = null;
  isEdit: boolean = false;
  isActived: boolean = true;
  sort: number | null = null;
}
