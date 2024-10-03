import {ModelBase} from "../../../base/modelBase";

export class Province extends ModelBase {
  code: string | null = null;
  name: string | null = null
  nameEn: string | null = null;
  fullName: string | null = null;
  fullNameEn: string | null = null;
  codeName: string | null = null;
  administrativeUnit: string | null = null;
  administrativeUnitId: number | null = null;
  administrativeRegion: string | null = null;
  administrativeRegionId: number | null = null;
}

// export function createDefaultProvince(): Province {
//   let modelBase: ModelBase = createDefaultModelBase();
//   return {
//     code: null,
//     name: null,
//     nameEn: null,
//     fullName: null,
//     fullNameEn: null,
//     codeName: null,
//     administrativeUnit: null,
//     administrativeUnitId: null,
//     administrativeRegion: null,
//     administrativeRegionId: null,
//     ...modelBase
//   };
// }
