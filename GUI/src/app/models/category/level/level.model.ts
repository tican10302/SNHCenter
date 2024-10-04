import { FormControl, FormGroup, Validators } from "@angular/forms";
import * as uuid from 'uuid';
import {ToFormControls} from "../../base/form-group.model";

export type LevelForm = ToFormControls<LevelModel>;

export class LevelModel {
  id: string | null = null;
  name: string | null = null;
  fee: number | null = null;
  note: string | null = null;
}

export function createDefaultLevelForm() {
  return new FormGroup<LevelForm>(<LevelForm>{
    id: new FormControl(uuid.v4(), { validators: [Validators.required] }),
    name: new FormControl('', { validators: [Validators.required] }),
    note: new FormControl(''),
  });
}
