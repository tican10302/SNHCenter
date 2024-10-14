import {ToFormControls} from "../base/form-group.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as uuid from "uuid";
import {GetListRequestModel} from "../base/get-list-request.model";

export type LessonTemplateForm = ToFormControls<LessonTemplateModel>;

export class LessonTemplateModel {
  id: string | null = null;
  courseTemplateId: string | null = null;
  lessonNo: number | null = null;
  hourDone: number | null = null;
  courseBookPage: string | null = null;
  lessonAim: string | null = null;
  additionalInformation: string | null = null;
}

export class GetListLessonTemplateRequestModel extends GetListRequestModel{
  courseTemplateId: string | null = null;
}

export function createDefaultLessonTemplateForm() {
  return new FormGroup<LessonTemplateForm>(<LessonTemplateForm>{
    id: new FormControl(uuid.v4(), {validators: [Validators.required]}),
    courseTemplateId: new FormControl(uuid.v4(), {validators: [Validators.required]}),
    lessonNo: new FormControl(0, {validators: [Validators.required]}),
    hourDone: new FormControl(0, {validators: [Validators.required]}),
    courseBookPage: new FormControl(''),
    lessonAim: new FormControl(''),
    additionalInformation: new FormControl(''),
  });
}
