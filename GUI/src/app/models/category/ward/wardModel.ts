import { FormControl, FormGroup } from "@angular/forms";
import { ToFormControls } from "../../base/ModelFormGroup";

export type WardForm = ToFormControls<WardModel>;

export class WardModel {
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

export function createDefaultWardForm() {
  return new FormGroup<WardForm>(<WardForm>{
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
