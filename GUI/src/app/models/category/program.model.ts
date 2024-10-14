import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ToFormControls} from "../base/form-group.model";
import * as uuid from 'uuid';

export type ProgramForm = ToFormControls<ProgramModel>;

export class ProgramModel {
  id: string | null = null;
  name: string | null = null;
  note: string | null = null;
}

export function createDefaultProgramForm() {
  return new FormGroup<ProgramForm>(<ProgramForm>{
    id: new FormControl(uuid.v4(), {validators: [Validators.required]}),
    name: new FormControl('', {validators: [Validators.required]}),
    note: new FormControl(''),
  });
}
