import { FormControl, FormGroup } from "@angular/forms";
import { ToFormControls } from "../../base/ModelFormGroup";

export type DistrictForm = ToFormControls<DistrictModel>;

export class DistrictModel {
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

export function createDefaultDistrictForm() {
  return new FormGroup<DistrictForm>(<DistrictForm>{
    code: new FormControl(''),
    name: new FormControl(''),
    nameEn: new FormControl(''),
    fullName: new FormControl(''),
    fullNameEn: new FormControl(''),
    codeName: new FormControl(''),
    administrativeUnit: new FormControl(''),
    administrativeUnitId: new FormControl(0),
    administrativeRegion: new FormControl(''),
    administrativeRegionId: new FormControl(0),
  });
}
