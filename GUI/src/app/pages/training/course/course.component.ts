import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { AccountService } from "../../../services/system/account.service";
import { PermissionModel } from "../../../models/system/permission.model";
import { DecimalPipe, NgFor, NgIf } from "@angular/common";
import { ButtonModule } from "primeng/button";
import { DialogModule } from "primeng/dialog";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { IconFieldModule } from "primeng/iconfield";
import { InputIconModule } from "primeng/inputicon";
import { InputTextModule } from "primeng/inputtext";
import { Table, TableModule } from "primeng/table";
import { TableColumnModel } from "../../../models/base/table-column.model";
import { GetListRequestModel } from "../../../models/base/get-list-request.model";
import { ConfirmationService, MessageService } from "primeng/api";
import { Enum } from "../../../enums/enum";
import { CourseService } from "../../../services/training/course.service";
import { createDefaultCourseForm, GetListCourseRequestModel, CourseModel } from "../../../models/training/course.model";
import { createFormGroup } from "../../../models/base/form-group.model";
import { NgxSpinnerService } from "ngx-spinner";
import { TextareaModule } from "primeng/textarea";
import { DatePickerModule } from 'primeng/datepicker';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';
import { createDefaultLevelForm, LevelModel } from "../../../models/category/level.model";
import { InputNumberModule } from "primeng/inputnumber";
import { Select } from "primeng/select";
import { SelectListItem } from "../../../models/base/select-list-item.model";
import { LevelService } from "../../../services/category/level.service";
import { ShiftService } from "../../../services/category/shift.service";


@Component({
  selector: 'app-course',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    TableModule,
    IconFieldModule,
    InputIconModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    TextareaModule,
    DatePickerModule,
    MultiSelectModule,
    InputNumberModule,
    DecimalPipe,
    Select,
  ],
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss']
})

export class CourseComponent implements OnInit {
  [x: string]: any;
  @ViewChild('dataTable') dataTable!: Table;
  permission: PermissionModel | null = null;
  formGroup = createDefaultCourseForm();
  visible: boolean = false;
  isView: boolean = false;
  isEdit: boolean = false;
  startDate: Date | null = null;
  endDate: Date | null = null;
  levelCombobox: SelectListItem[] = [];
  shiftCombobox: SelectListItem[] = [];
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: CourseModel[];
  selectedListData!: CourseModel;
  cols!: TableColumnModel[];
  totalRecords: number = 0;

  getListPagingRequest = new GetListCourseRequestModel();

  constructor(protected accountService: AccountService,
    private courseService: CourseService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private spinner: NgxSpinnerService,
    private levelService: LevelService,
    private shiftService: ShiftService,) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'name', header: 'Name' },
      { field: 'startDate', header: 'Start Date' },
      { field: 'endDate', header: 'End Date' },
      { field: 'center', header: 'Center' },
      { field: 'room', header: 'Room' },
      { field: 'level', header: 'Level' },
      { field: 'shift', header: 'Shift' },
      { field: 'note', header: 'Note' },
    ];
    this.levelService.getCombobox({}).subscribe({
      next: (data) => {
        this.levelCombobox = [{ text: '-- Level --', value: null }, ...data];
      }
    })
    this.shiftService.getCombobox({}).subscribe({
      next: (data) => {
        this.shiftCombobox = [{ text: '-- Shift --', value: null }, ...data];
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
    if (event) {
      this.getListPagingRequest.offset = event.first;
      this.getListPagingRequest.limit = event.rows;
    }

    this.courseService.getListData(this.getListPagingRequest).subscribe({
      next: (data) => {
        this.tableData = data.data;
        this.totalRecords = data.totalRow;
      },
      error: (err) => this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
    });

    // Reset form
    this.formGroup = createDefaultCourseForm();
  }

  showViewDialog() {
    this.formGroup = createDefaultCourseForm();
    let id = this.getIdSelections('view')[0];
    if (!id) return;

    this.courseService.getData(id).subscribe({
      next: (data) => {
        this.formGroup = createFormGroup(data);
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife });
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
    let id = this.getIdSelections('edit')[0];
    if (!id) return;

    this.courseService.getData(id).subscribe({
      next: (data) => {
        this.formGroup = createFormGroup(data);
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife });
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
    this.formGroup = createDefaultCourseForm();
  }

  saveData() {
    if (this.formGroup.invalid) return;
    this.spinner.show();

    if (this.isEdit) {
      this.courseService.updateData(this.formGroup.value).subscribe({
        next: _ => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `Updated successfully`, life: Enum.messageLife });
          this.loadData(null);
          this.spinner.hide();
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife });
          this.spinner.hide();
        }
      });
    } else {
      this.courseService.addData(this.formGroup.value).subscribe({
        next: _ => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `Created successfully`, life: Enum.messageLife });
          this.loadData(null);
          this.spinner.hide();
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife });
          this.spinner.hide();
        }
      });
    }

    this.isView = false;
    this.isEdit = false;
    this.visible = false;
  }

  deleteData(ids: string[]) {
    this.spinner.show();
    this.courseService.deleteData(ids).subscribe({
      next: _ => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: `Deleted successfully`, life: Enum.messageLife });
        this.loadData(null);
        this.spinner.hide();
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife });
        this.spinner.hide();
      }
    });
  }

  getIdSelections(action: string, multi: boolean = false) {
    let selects: string[] = [];
    if (Array.isArray(this.selectedListData) && this.selectedListData.length >= 1) {
      selects = this.selectedListData.map(el => el.id);
    }

    if (multi) {
      if (!(selects.length >= 1)) {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: `Please select rows to ${action}`, life: Enum.messageLife });
        return [];
      }
    } else {
      if (!(selects.length === 1)) {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: `Please select a row to ${action}`, life: Enum.messageLife });
        return [];
      }
    }
    return selects;
  }

  confirmDelete(event: Event) {
    let ids = this.getIdSelections('delete', true);
    if (!ids || ids.length === 0) return;

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
  onLevelChange(selectedValue: any) {
    this.getListPagingRequest.levelId = selectedValue;
    this.loadData(null);
  }
  onShiftChange(selectedValue: any) {
    this.getListPagingRequest.shiftId = selectedValue;
    this.loadData(null);
  }

}
