import {FormArray, FormControl, FormGroup, Validators} from "@angular/forms";
import * as uuid from 'uuid';
import {ToFormControls} from "../base/form-group.model";

export type ShiftForm = ToFormControls<ShiftDto>;

export class ShiftModel {
  id: string | null = null;
  name: string | null = null;
  time: Date  | null = null;
  days: string | null = null;
  note: string | null = null;
}

export class ShiftDto {
  id: string | null = null;
  name: string | null = null;
  time: Date | null = null;
  days: string | null = null;
  note: string | null = null;
  Monday: boolean = false;
  Tuesday: boolean = false;
  Wednesday: boolean = false;
  Thursday: boolean = false;
  Friday: boolean = false;
  Saturday: boolean = false;
  Sunday: boolean = false;
}

export function createDefaultShiftForm(): FormGroup<ShiftForm> {

  return new FormGroup<ShiftForm>({
    id: new FormControl(uuid.v4(), { validators: [Validators.required] }),
    name: new FormControl('', { validators: [Validators.required] }),
    time: new FormControl(null, { validators: [Validators.required] }),
    days: new FormControl(''),
    note: new FormControl(''),
    Monday: new FormControl(false),
    Tuesday: new FormControl(false),
    Wednesday: new FormControl(false),
    Thursday: new FormControl(false),
    Friday: new FormControl(false),
    Saturday: new FormControl(false),
    Sunday: new FormControl(false),
  } as ShiftForm);
}
