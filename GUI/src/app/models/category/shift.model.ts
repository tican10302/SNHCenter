import { FormControl, FormGroup, Validators } from "@angular/forms";
import * as uuid from 'uuid';
import {ToFormControls} from "../base/form-group.model";

export type ShiftForm = ToFormControls<ShiftModel>;

export class ShiftModel {
  id: string | null = null;
  name: string | null = null;
  time: Date | null = null;
  day: string | null = null;
  note: string | null = null;
}

export function createDefaultShiftForm() {
  return new FormGroup<ShiftForm>(<ShiftForm>{
    id: new FormControl(uuid.v4(), { validators: [Validators.required] }),
    name: new FormControl('', { validators: [Validators.required] }),
    time: new FormControl(null, { validators: [Validators.required] }),
    day: new FormControl('', { validators: [Validators.required] }),
    note: new FormControl(''),
  });
}
