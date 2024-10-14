import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { AccountService } from "../../../services/system/account.service";
import { NgFor, NgIf } from "@angular/common";
import { ButtonModule } from "primeng/button";
import { DialogModule } from "primeng/dialog";
import {FormArray, FormControl, FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import { IconFieldModule } from "primeng/iconfield";
import { InputIconModule } from "primeng/inputicon";
import { InputTextModule } from "primeng/inputtext";
import { Table, TableModule } from "primeng/table";
import { ConfirmationService, MessageService } from "primeng/api";
import { Enum } from "../../../enums/enum";
import { ShiftService } from "../../../services/category/shift.service";
import {createDefaultShiftForm, ShiftDto, ShiftModel} from "../../../models/category/shift.model";
import { NgxSpinnerService } from "ngx-spinner";
import { TextareaModule } from "primeng/textarea";
import {PermissionModel} from "../../../models/system/permission.model";
import {TableColumnModel} from "../../../models/base/table-column.model";
import {GetListRequestModel} from "../../../models/base/get-list-request.model";
import {createFormGroup} from "../../../models/base/form-group.model";
import {DatePickerModule} from "primeng/datepicker";
import {CheckboxModule} from "primeng/checkbox";
@Component({
  selector: 'app-shift',
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
    CheckboxModule,
  ],
  templateUrl: './shift.component.html',
  styleUrl: './shift.component.scss'
})
export class ShiftComponent implements OnInit {
  @ViewChild('dataTable') dataTable!: Table;
  permission: PermissionModel | null = null;
  formGroup = createDefaultShiftForm();
  visible: boolean = false;
  isView: boolean = false;
  isEdit: boolean = false;
  selectDays: string[] = [];
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: ShiftModel[];
  selectedListData!: ShiftModel;
  cols!: TableColumnModel[];
  totalRecords: number = 0;

  getListPagingRequest = new GetListRequestModel();


  constructor(protected accountService: AccountService,
    private shiftService: ShiftService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private spinner: NgxSpinnerService,) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'name', header: 'Name' },
      { field: 'time', header: 'Time' },
      { field: 'days', header: 'Days' },
      { field: 'note', header: 'Note' },
    ];
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

    this.shiftService.getListData(this.getListPagingRequest).subscribe({
      next: (data) => {
        this.tableData = data.data;
        this.totalRecords = data.totalRow;
      },
      error: (err) => this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
    });

    // Delete data on Form
    this.formGroup = createDefaultShiftForm();
  }

  showViewDialog() {
    this.formGroup = createDefaultShiftForm();
    let id = this.getIdSelections('view')[0];
    if (!id)
      return;
    this.shiftService.getData(id).subscribe({
      next: (data) => {
        let dto = this.convertModelToDto(data);

        // Set time to date
        const time: Date = new Date();
        const timeString = dto.time?.toString() || '00:00';
        const [hours, minutes] = timeString.split(':').map(Number);

        time.setHours(hours, minutes, 0);
        dto.time = time;

        this.formGroup = createFormGroup(dto);
        this.selectDays = data.days ? data.days.split(', ') : [];
        console.log(this.formGroup);
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
      }
    });

    this.isView = true;
    this.isEdit = false;
    this.visible = true;
  }

  showAddDialog() {
    // Delete data on Form
    this.formGroup = createDefaultShiftForm();

    this.isView = false;
    this.isEdit = false;
    this.visible = true;
    this.selectDays = [];
  }

  showEditDialog() {
    let id = this.getIdSelections('view')[0];
    if (!id)
      return;
    this.shiftService.getData(id).subscribe({
      next: (data) => {
        let dto = this.convertModelToDto(data);

        // Set time to date
        const time: Date = new Date();
        const timeString = dto.time?.toString() || '00:00:00';
        const [hours, minutes, seconds] = timeString.split(':').map(Number);

        time.setHours(hours, minutes, seconds, 0);
        dto.time = time;

        this.formGroup = createFormGroup(dto);
        this.selectDays = data.days ? data.days.split(',') : [];
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
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
    this.selectDays = [];
    this.formGroup = createDefaultShiftForm();
  }

  saveData() {
    if (this.formGroup.invalid) return;
    this.spinner.show();

    // convert dto to model
    let data = this.convertFormGroupToModel(this.formGroup);
    if (this.isEdit)
      this.shiftService.updateData(data).subscribe({
        next: _ => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `Update data successfully`, life: Enum.messageLife });
          this.loadData(null);
          this.spinner.hide();
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
          this.spinner.hide();
        }
      });
    else
      this.shiftService.addData(data).subscribe({
        next: _ => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `Create data successfully`, life: Enum.messageLife });
          this.loadData(null);
          this.spinner.hide();
        },
        error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
          this.spinner.hide();
        }
      });
    this.isView = false;
    this.isEdit = false;
    this.visible = false;
    this.selectDays = [];

  }

  deleteData(ids: string[]) {
    this.spinner.show();
    this.shiftService.deleteData(ids).subscribe({
      next: _ => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: `Delete data successfully`, life: Enum.messageLife });
        this.loadData(null);
        this.spinner.hide();
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
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
    if (!ids || ids.length === 0)
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

  convertModelToDto(model: ShiftModel): ShiftDto {
    return {
      id: model.id,
      name: model.name,
      time: model.time,
      days: model.days,
      note: model.note,
      Monday: !!model.days?.includes('Monday'),
      Tuesday: !!model.days?.includes('Tuesday'),
      Wednesday: !!model.days?.includes('Wednesday'),
      Thursday: !!model.days?.includes('Thursday'),
      Friday: !!model.days?.includes('Friday'),
      Saturday: !!model.days?.includes('Saturday'),
      Sunday: !!model.days?.includes('Sunday')
    }
  }

  convertFormGroupToModel(formGroup: FormGroup): ShiftModel {
    const formValue = formGroup.value;

    const formattedTime = formValue.time.toLocaleTimeString('en-GB', {
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
      hour12: false
    });

    console.log(formattedTime)
    let daysArray: string[] = [];

    if (formValue.Monday) daysArray.push('Monday');
    if (formValue.Tuesday) daysArray.push('Tuesday');
    if (formValue.Wednesday) daysArray.push('Wednesday');
    if (formValue.Thursday) daysArray.push('Thursday');
    if (formValue.Friday) daysArray.push('Friday');
    if (formValue.Saturday) daysArray.push('Saturday');
    if (formValue.Sunday) daysArray.push('Sunday');

    return {
      id: formValue.id,
      name: formValue.name,
      time: formattedTime,
      days: daysArray.join(', '),
      note: formValue.note
    };
  }

  onCheckboxChangeDay(day: string) {
    const selected = this.formGroup.get(day)?.value;
    if (selected) {
      // Add day to selected days array
      if (!this.selectDays.includes(day)) {
        this.selectDays.push(day);
      }
    } else {
      // Remove day from selected days array
      const index = this.selectDays.indexOf(day);
      if (index !== -1) {
        this.selectDays.splice(index, 1);
      }
    }
  }

}
