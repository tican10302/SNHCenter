import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ToFormControls } from "../base/form-group.model";
import * as uuid from 'uuid';
import { GetListRequestModel } from "../base/get-list-request.model";

export type CourseForm = ToFormControls<CourseModel>;

export class CourseModel {
  id: string | null = null;
  name: string | null = null;
  startDate: Date | null = null;
  endDate: Date | null = null;
  center: string | null = null;
  room: string | null = null;
  shiftId: string | null = null;
  levelId: string | null = null;
  note: string | null = null;
}

export class GetListCourseRequestModel extends GetListRequestModel {
  shiftId: string | null = null;
  levelId: string | null = null;

}

export function createDefaultCourseForm() {
  return new FormGroup<CourseForm>(<CourseForm>{
    id: new FormControl(uuid.v4(), { validators: [Validators.required] }),
    name: new FormControl('', { validators: [Validators.required] }),
    startDate: new FormControl(null, { validators: [Validators.required] }),
    endDate: new FormControl(null),
    center: new FormControl('', { validators: [Validators.required] }),
    room: new FormControl('', { validators: [Validators.required] }),
    shiftId: new FormControl(null, { validators: [Validators.required] }),
    levelId: new FormControl(null, { validators: [Validators.required] }),
    note: new FormControl('')
  });
}
