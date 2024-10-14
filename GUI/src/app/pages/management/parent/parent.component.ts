import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { AccountService } from "../../../services/system/account.service";
import { PermissionModel } from "../../../models/system/permission.model";
import { NgFor, NgIf } from "@angular/common";
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
import { ParentService } from "../../../services/management/parent.service";
import { createDefaultParentForm, ParentModel } from "../../../models/management/parent.model";
import { createFormGroup } from "../../../models/base/form-group.model";
import { NgxSpinnerService } from "ngx-spinner";
import { TextareaModule } from "primeng/textarea";
import { Select } from "primeng/select";
import { SelectListItem } from "../../../models/base/select-list-item.model";
import { ProvinceService } from "../../../services/category/province.service";
import { DistrictService } from "../../../services/category/district.service";
import { WardService } from "../../../services/category/ward.service";

@Component({
  selector: 'app-parent',
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
    Select,
  ],
  templateUrl: './parent.component.html',
  styleUrl: './parent.component.scss'
})
export class ParentComponent implements OnInit {
  @ViewChild('dataTable') dataTable!: Table;
  permission: PermissionModel | null = null;
  formGroup = createDefaultParentForm();
  visible: boolean = false;
  isView: boolean = false;
  isEdit: boolean = false;
  provinceCombobox: SelectListItem[] = [];
  districtCombobox: SelectListItem[] = [];
  wardCombobox: SelectListItem[] = [];
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: ParentModel[];
  selectedListData!: ParentModel;
  cols!: TableColumnModel[];
  totalRecords: number = 0;

  getListPagingRequest = new GetListRequestModel();


  constructor(protected accountService: AccountService,
    private parentService: ParentService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private spinner: NgxSpinnerService,
    private provinceService: ProvinceService,
    private districtService: DistrictService,
    private wardService: WardService,) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'firstName', header: 'First Name' },
      { field: 'lastName', header: 'Last Name' },
      { field: 'phone', header: 'Phone' },
      { field: 'email', header: 'Email' },
      { field: 'province', header: 'Province' },
      { field: 'district', header: 'District' },
      { field: 'ward', header: 'Ward' },
      { field: 'note', header: 'Note' },
    ];
    this.provinceService.getCombobox({}).subscribe({
      next: (data) => {
        this.provinceCombobox = [{ text: '-- Group province --', value: null }, ...data];
      }
    })
    this.districtService.getCombobox({}).subscribe({
      next: (data) => {
        this.districtCombobox = [{ text: '-- Group district --', value: null }, ...data];
      }
    })
    this.wardService.getCombobox({}).subscribe({
      next: (data) => {
        this.wardCombobox = [{ text: '-- Group ward --', value: null }, ...data];
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

    this.parentService.getListData(this.getListPagingRequest).subscribe({
      next: (data) => {
        this.tableData = data.data;
        this.totalRecords = data.totalRow;
      },
      error: (err) => this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
    });

    // Delete data on Form
    this.formGroup = createDefaultParentForm();
  }

  showViewDialog() {
    this.formGroup = createDefaultParentForm();
    let id = this.getIdSelections('view')[0];
    if (!id)
      return;
    this.parentService.getData(id).subscribe({
      next: (data) => {
        this.formGroup = createFormGroup(data);
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
    this.isView = false;
    this.isEdit = false;
    this.visible = true;
  }

  showEditDialog() {
    let id = this.getIdSelections('view')[0];
    if (!id)
      return;
    this.parentService.getData(id).subscribe({
      next: (data) => {
        this.formGroup = createFormGroup(data);
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
    this.formGroup = createDefaultParentForm();
  }

  saveData() {
    if (this.formGroup.invalid) return;
    this.spinner.show();
    if (this.isEdit)
      this.parentService.updateData(this.formGroup.value).subscribe({
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
      this.parentService.addData(this.formGroup.value).subscribe({
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

  }

  deleteData(ids: string[]) {
    this.spinner.show();
    this.parentService.deleteData(ids).subscribe({
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
  onProvinceChange(selectedValue: any) {
    this.getListPagingRequest.provinceId = selectedValue;
    this.loadData(null);
  }
  onDistrictChange(selectedValue: any) {
    this.getListPagingRequest.districtId = selectedValue;
    this.loadData(null);
  }
  onWardChange(selectedValue: any) {
    this.getListPagingRequest.wardId = selectedValue;
    this.loadData(null);
  }
}
