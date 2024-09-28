import {ModelBase} from "../../modelBase";
import {GetListPagingResponse} from "../../base/getListPagingResponse";

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
