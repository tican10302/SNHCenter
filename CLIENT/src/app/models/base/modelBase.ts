import {Province} from "../category/province/models/province";

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

export function createDefaultModelBase(): ModelBase {
  return {
    id: '',
    createdAt: null,
    createdBy: null,
    updatedAt: null,
    updatedBy: null,
    isEdit: false,
    isActived: true,
    sort: null,
  };
}
