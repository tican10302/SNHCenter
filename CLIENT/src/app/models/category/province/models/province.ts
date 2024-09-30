import {createDefaultModelBase, ModelBase} from "../../../base/modelBase";

export interface Province extends ModelBase {
  code: string | null;
  name: string | null;
  nameEn: string | null;
  fullName: string | null;
  fullNameEn: string | null;
  codeName: string | null;
  administrativeUnit: string | null;
  administrativeUnitId: number | null;
  administrativeRegion: string | null;
  administrativeRegionId: number | null;
}

export function createDefaultProvince(): Province {
  let modelBase: ModelBase = createDefaultModelBase();
  return {
    code: null,
    name: null,
    nameEn: null,
    fullName: null,
    fullNameEn: null,
    codeName: null,
    administrativeUnit: null,
    administrativeUnitId: null,
    administrativeRegion: null,
    administrativeRegionId: null,
    ...modelBase
  };
}
