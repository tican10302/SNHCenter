import {FormControl, Validators} from "@angular/forms";

type ToFormControls<T> = {
  [K in keyof T]: FormControl<T[K]>;
};

type DtoBaseForm = ToFormControls<DtoBase>;

export class DtoBase
{
  id: string | null = null;
  folderUpload: string| null = null;
  isActived: boolean = true;
  isEdit: boolean = false;
  sort: number = 0;
}

export const baseFormControls = <DtoBaseForm>{
  id: new FormControl('', { validators: [Validators.required] }),
  folderUpload: new FormControl(''),
  isActived: new FormControl(true),
  isEdit: new FormControl(false),
  sort: new FormControl(0),
};
