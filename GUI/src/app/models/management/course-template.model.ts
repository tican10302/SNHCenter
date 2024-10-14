import {ToFormControls} from "../base/form-group.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as uuid from "uuid";

export type CourseTemplateForm = ToFormControls<CourseTemplateModel>;

export class CourseTemplateModel {
  id: string | null = null;
  levelId: string | null = null;
}

export function createDefaultCourseTemplateForm() {
  return new FormGroup<CourseTemplateForm>(<CourseTemplateForm>{
    id: new FormControl(uuid.v4(), {validators: [Validators.required]}),
    levelId: new FormControl('', {validators: [Validators.required]}),
  });
}
