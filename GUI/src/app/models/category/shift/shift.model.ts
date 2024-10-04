import { FormControl, FormGroup, Validators } from "@angular/forms";
import * as uuid from 'uuid';
import {ToFormControls} from "../../base/form-group.model";

export type ShiftForm = ToFormControls<ShiftModel>;
export interface TimeSpan {
  startTime: string;  // Ví dụ: "08:00"
  endTime: string;    // Ví dụ: "17:00"
}

export class ShiftModel {
  id: string | null = null;
  name: string | null = null;
  time: TimeSpan | null = null;
  day: string | null = null;
  note: string | null = null;
}

export function createDefaultShiftForm() {
  return new FormGroup<ShiftForm>(<ShiftForm>{
    id: new FormControl(uuid.v4(), { validators: [Validators.required] }),
    name: new FormControl('', { validators: [Validators.required] }),
    note: new FormControl(''),
  });
}
