import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { NgFor, NgIf } from "@angular/common"
import { AccountService } from "../../../services/system/account.service";
import { ActivatedRoute } from "@angular/router";
import { Table, TableModule } from "primeng/table";
import { DistrictService } from "../../../services/category/district.service";
import { MessageService } from "primeng/api";
import { IconFieldModule } from "primeng/iconfield";
import { InputIconModule } from "primeng/inputicon";
import { ButtonModule } from "primeng/button";
import { DialogModule } from "primeng/dialog";
import { InputTextModule } from "primeng/inputtext";
import { Enum } from "../../../enums/enum";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { createDefaultDistrictForm, DistrictModel } from '../../../models/category/district.model';
import { NgxSpinnerService } from 'ngx-spinner';
import {TableColumnModel} from "../../../models/base/table-column.model";
import {GetListRequestModel} from "../../../models/base/get-list-request.model";
import {PermissionModel} from "../../../models/system/permission.model";
import {createFormGroup} from "../../../models/base/form-group.model";

@Component({
  selector: 'app-district',
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
  ],
  templateUrl: './district.component.html',
  styleUrl: './district.component.scss'
})
export class DistrictComponent implements OnInit {
  @ViewChild('dataTable') dataTable!: Table;
  permission: PermissionModel | null = null;
  formGroup = createDefaultDistrictForm();
  visible: boolean = false;
  isEdit: boolean = false;
  isView: boolean = false;
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: DistrictModel[];
  selectedListData!: DistrictModel;
  cols!: TableColumnModel[];
  totalRecords: number = 0;

  getListPagingRequest = new GetListRequestModel();

  constructor(protected accountService: AccountService,
    private districtService: DistrictService,
    private messageService: MessageService,
    private spinner: NgxSpinnerService,) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'code', header: 'Code' },
      { field: 'name', header: 'Name' },
      { field: 'fullName', header: 'Full name' }
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

    this.districtService.getListData(this.getListPagingRequest).subscribe({
      next: (data) => {
        this.tableData = data.data;
        this.totalRecords = data.totalRow;
      },
      error: (err) => this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
    });

    // Delete data on Form
    this.formGroup = createDefaultDistrictForm();
  }

  showViewDialog() {
    let id = this.getIdSelections('view')[0];
    if (!id)
      return;
    this.districtService.getData(id).subscribe({
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

  saveData() {
    this.isView = false;
    this.isEdit = false;
    this.visible = true;
  }

  closeDialog() {
    this.isView = false;
    this.isEdit = false;
    this.visible = false;
    this.formGroup = createDefaultDistrictForm();
  }

  getIdSelections(action: string, multi: boolean = false) {
    let selects: string[] = [];
    if (Array.isArray(this.selectedListData) && this.selectedListData.length >= 1) {
      selects = this.selectedListData.map(el => {
        return el.code;
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
}
