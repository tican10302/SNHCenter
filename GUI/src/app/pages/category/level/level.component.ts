import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { AccountService } from "../../../services/system/account.service";
import { Permission } from "../../../models/system/permission";
import { NgFor, NgIf } from "@angular/common";
import { ButtonModule } from "primeng/button";
import { DialogModule } from "primeng/dialog";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { IconFieldModule } from "primeng/iconfield";
import { InputIconModule } from "primeng/inputicon";
import { InputTextModule } from "primeng/inputtext";
import { Table, TableModule } from "primeng/table";
import { TableColumn } from "../../../models/base/tableColumn";
import { CreateDefaultGetListPagingRequest, GetListPagingRequest } from "../../../models/base/getListPagingRequest";
import { ConfirmationService, MessageService } from "primeng/api";
import { Enum } from "../../../enums/enum";
import { LevelService } from "../../../services/category/level.service";
import { createDefaultLevelForm, LevelModel } from "../../../models/category/level/levelModel";
import { createFormGroup } from "../../../models/base/ModelFormGroup";
import { NgxSpinnerService } from "ngx-spinner";
import { TextareaModule } from "primeng/textarea";

@Component({
  selector: 'app-level',
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
  ],
  templateUrl: './level.component.html',
  styleUrl: './level.component.scss'
})
export class LevelComponent implements OnInit {
  @ViewChild('dataTable') dataTable!: Table;
  permission: Permission | null = null;
  formGroup = createDefaultLevelForm();
  visible: boolean = false;
  isView: boolean = false;
  isEdit: boolean = false;
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: LevelModel[];
  selectedListData!: LevelModel;
  cols!: TableColumn[];
  totalRecords: number = 0;

  getListPagingRequest: GetListPagingRequest = CreateDefaultGetListPagingRequest();


  constructor(protected accountService: AccountService,
    private levelService: LevelService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private spinner: NgxSpinnerService,) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'name', header: 'Name' },
      { field: 'fee', header: 'Fee' },
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

    this.levelService.getListData(this.getListPagingRequest).subscribe({
      next: (data) => {
        this.tableData = data.data;
        this.totalRecords = data.totalRow;
      },
      error: (err) => this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife })
    });

    // Delete data on Form
    this.formGroup = createDefaultLevelForm();
  }

  showViewDialog() {
    let id = this.getIdSelections('view')[0];
    if (!id)
      return;
    this.levelService.getData(id).subscribe({
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
    this.levelService.getData(id).subscribe({
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
    this.formGroup = createDefaultLevelForm();
  }

  saveData() {
    if (this.formGroup.invalid) return;
    this.spinner.show();
    if (this.isEdit)
      this.levelService.updateData(this.formGroup.value).subscribe({
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
      this.levelService.addData(this.formGroup.value).subscribe({
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
    this.levelService.deleteData(ids).subscribe({
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

}
