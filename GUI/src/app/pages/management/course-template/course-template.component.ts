import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {Table, TableModule} from "primeng/table";
import {PermissionModel} from "../../../models/system/permission.model";
import {ActivatedRoute} from "@angular/router";
import {TableColumnModel} from "../../../models/base/table-column.model";
import {GetListRequestModel} from "../../../models/base/get-list-request.model";
import {AccountService} from "../../../services/system/account.service";
import {ConfirmationService, MessageService} from "primeng/api";
import {NgxSpinnerService} from "ngx-spinner";
import {Enum} from "../../../enums/enum";
import {createFormGroup} from "../../../models/base/form-group.model";
import {CourseTemplateModel, createDefaultCourseTemplateForm} from "../../../models/management/course-template.model";
import {CourseTemplateService} from "../../../services/management/course-template.service";
import {ButtonModule} from "primeng/button";
import {IconFieldModule} from "primeng/iconfield";
import {InputIconModule} from "primeng/inputicon";
import {InputTextModule} from "primeng/inputtext";
import {NgFor, NgIf} from "@angular/common";
import {DialogModule} from "primeng/dialog";
import {ReactiveFormsModule} from "@angular/forms";
import {TextareaModule} from "primeng/textarea";
import {SelectListItem} from "../../../models/base/select-list-item.model";
import {LevelService} from "../../../services/category/level.service";
import {Select} from "primeng/select";
import {LessonTemplateModel} from "../../../models/management/lesson-template.model";
import {LessonTemplateService} from "../../../services/management/lesson-template.service";

@Component({
  selector: 'app-course-template',
  standalone: true,
  imports: [
    TableModule,
    ButtonModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    NgFor,
    DialogModule,
    ReactiveFormsModule,
    NgIf,
    TextareaModule,
    Select
  ],
  templateUrl: './course-template.component.html',
  styleUrl: './course-template.component.scss'
})
export class CourseTemplateComponent implements OnInit{
  @ViewChild('dataTable') dataTable!: Table;
  permission: PermissionModel | null = null;
  formGroup = createDefaultCourseTemplateForm();
  visible: boolean = false;
  isView: boolean = false;
  isEdit: boolean = false;
  levelCombobox: SelectListItem[] = [];
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: CourseTemplateModel[];
  selectedListData!: CourseTemplateModel;
  cols!: TableColumnModel[];
  totalRecords: number = 0;

  // Table Lesson Template
  tableDataLessonTemplate!: LessonTemplateModel[];
  selectedListDataLessonTemplate!: LessonTemplateModel;
  colsLessonTemplate!: TableColumnModel[];
  totalRecordsLessonTemplate: number = 0;

  getListPagingRequest = new GetListRequestModel();


  constructor(protected accountService: AccountService,
              private courseTemplateService: CourseTemplateService,
              private lessonTemplateService: LessonTemplateService,
              private messageService: MessageService,
              private confirmationService: ConfirmationService,
              private spinner: NgxSpinnerService,
              private levelService: LevelService) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'level', header: 'Level' },
      { field: 'createdAt', header: 'Created At' },
      { field: 'createdBy', header: 'Created By' },
      { field: 'isActive', header: 'Status' },
    ];

    this.colsLessonTemplate = [
      { field: 'lessonNo', header: 'Lesson No' },
      { field: 'hourDone', header: 'Hour Done' },
      { field: 'courseBookPage', header: 'Course Book Page' },
      { field: 'lessonAim', header: 'Lesson Aim' },
      { field: 'additionalInformation', header: 'Additional Information' },
    ];

    this.levelService.getCombobox({}).subscribe({
      next: (data) => {
        this.levelCombobox = [{ text: '-- Level --', value: null }, ...data];
      }
    })
  }

  onSearch(event: Event) {
    const input = event.target as HTMLInputElement;
    this.getListPagingRequest.search = input.value;
    this.getListPagingRequest.offset = 0;
    this.loadData(null);
  }

  loadData(event: any | null) {
    if(event)
    {
      this.getListPagingRequest.offset = event.first;
      this.getListPagingRequest.limit = event.rows;
    }

    this.courseTemplateService.getListData(this.getListPagingRequest).subscribe({
      next: (data) => {
        this.tableData = data.data;
        this.totalRecords = data.totalRow;
      },
      error: (err) => this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
    });

    // Delete data on Form
    this.formGroup = createDefaultCourseTemplateForm();
  }

  loadDataLessonTemplate() {
    this.lessonTemplateService.getAllData(this.selectedListData.id || '').subscribe({
      next: (data) => {
        this.tableDataLessonTemplate = data;
      },
      error: (err) => this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
    });
  }

  showViewDialog() {
    this.formGroup = createDefaultCourseTemplateForm();
    let id = this.getIdSelections('view')[0];
    if(!id)
      return;
    this.courseTemplateService.getData(id).subscribe({
      next: (data) => {
        this.formGroup = createFormGroup(data);
      },
      error: (err) => {
        this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
      }
    });

    this.isView = true;
    this.isEdit = false;
    this.visible = true;
  }

  showAddDialog() {
    this.isView = false;
    this.isEdit = false;
    this.visible = true;
  }

  showEditDialog() {
    let id = this.getIdSelections('view')[0];
    if(!id)
      return;
    this.courseTemplateService.getData(id).subscribe({
      next: (data) => {
        this.formGroup = createFormGroup(data);
      },
      error: (err) => {
        this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
      }
    });

    this.isView = false;
    this.isEdit = true;
    this.visible = true;
  }

  closeDialog() {
    this.isView = false;
    this.isEdit = false;
    this.visible = false;
    this.formGroup = createDefaultCourseTemplateForm();
  }

  saveData() {
    if(this.formGroup.invalid) return;
    this.spinner.show();
    if(this.isEdit)
      this.courseTemplateService.updateData(this.formGroup.value).subscribe({
        next: _ => {
          this.messageService.add({severity: 'success', summary: 'Success', detail: `Update data successfully`, life: Enum.messageLife});
          this.loadData(null);
          this.spinner.hide();
        },
        error: (err) =>  {
          this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
          this.spinner.hide();
        }
      });
    else
      this.courseTemplateService.addData(this.formGroup.value).subscribe({
        next: _ => {
          this.messageService.add({severity: 'success', summary: 'Success', detail: `Create data successfully`, life: Enum.messageLife});
          this.loadData(null);
          this.spinner.hide();
        },
        error: (err) =>  {
          this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
          this.spinner.hide();
        }
      });
    this.isView = false;
    this.isEdit = false;
    this.visible = false;

  }

  deleteData(ids: string[]) {
    this.spinner.show();
    this.courseTemplateService.deleteData(ids).subscribe({
      next: _ => {
        this.messageService.add({severity: 'success', summary: 'Success', detail: `Delete data successfully`, life: Enum.messageLife});
        this.loadData(null);
        this.spinner.hide();
      },
      error: (err) =>  {
        this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
        this.spinner.hide();
      }
    });
  }

  getIdSelections(action: string, multi: boolean = false) {
    let selects: string[] = [];
    if (Array.isArray(this.selectedListData) && this.selectedListData.length >= 1) {
      selects = this.selectedListData.map(el => {
        return el.id;
      })
    }
    if(multi) {
      if(!(selects.length >= 1)) {
        this.messageService.add({severity: 'error', summary: 'Error', detail: `Please select rows to ${action}`, life: Enum.messageLife});
        return [];
      }
    } else {
      if(!(selects.length === 1)) {
        this.messageService.add({severity: 'error', summary: 'Error', detail: `Please select a row to ${action}`, life: Enum.messageLife});
        return [];
      }
    }
    return selects;
  }

  confirmDelete(event: Event) {
    let ids = this.getIdSelections('delete', true);
    if(!ids || ids.length === 0)
      return;
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Do you want to delete this record?',
      header: 'Danger Zone',
      icon: 'pi pi-info-circle',
      rejectLabel: 'Cancel',
      rejectButtonProps: {
        label: 'Cancel',
        severity: 'secondary',
        outlined: true,
      },
      acceptButtonProps: {
        label: 'Delete',
        severity: 'danger',
      },

      accept: () => {
        this.deleteData(ids);
      },
    });
  }
}
