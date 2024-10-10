import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {NgFor, NgIf} from "@angular/common";
import {Table, TableModule} from "primeng/table";
import {IconFieldModule} from "primeng/iconfield";
import {InputIconModule} from "primeng/inputicon";
import {ButtonModule} from "primeng/button";
import {DialogModule} from "primeng/dialog";
import {InputTextModule} from "primeng/inputtext";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {TextareaModule} from "primeng/textarea";
import {PermissionModel} from "../../../models/system/permission.model";
import {ActivatedRoute} from "@angular/router";
import {TableColumnModel} from "../../../models/base/table-column.model";
import {GetListRequestModel} from "../../../models/base/get-list-request.model";
import {AccountService} from "../../../services/system/account.service";
import {ConfirmationService, MessageService} from "primeng/api";
import {NgxSpinnerService} from "ngx-spinner";
import {Enum} from "../../../enums/enum";
import {createFormGroup} from "../../../models/base/form-group.model";
import {createDefaultRoleForm, RoleModel} from "../../../models/system/role.model";
import {RoleService} from "../../../services/system/role.service";
import {
  createDefaultRolePermissionForm,
  GetListRolePermissionRequestModel, RolePermissionModel
} from "../../../models/system/role-permission.model";
import {TabPanel, Tabs} from "primeng/tabs";
import {GroupPermissionModel} from "../../../models/system/group-permission.model";

@Component({
  selector: 'app-role',
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
    Tabs,
    TabPanel,
  ],
  templateUrl: './role.component.html',
  styleUrl: './role.component.scss'
})
export class RoleComponent implements OnInit{
  @ViewChild('dataTable') dataTable!: Table;
  permission: PermissionModel | null = null;
  formGroup = createDefaultRoleForm();
  formGroupRolePermission = createDefaultRolePermissionForm();
  visible: boolean = false;
  visiblePermission: boolean = false;
  isView: boolean = false;
  isEdit: boolean = false;
  tabsRolePermission: GroupPermissionModel[] = [];
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: RoleModel[];
  selectedListData!: RoleModel;
  cols!: TableColumnModel[];
  totalRecords: number = 0;

  // Table Role Permission
  tableDataRolePermission!: RolePermissionModel[];
  selectedListDataRolePermission!: RolePermissionModel;
  colsRolePermission!: TableColumnModel[];
  totalRecordsRolePermission: number = 0;

  getListPagingRequest = new GetListRequestModel();


  constructor(protected accountService: AccountService,
              private roleService: RoleService,
              private messageService: MessageService,
              private confirmationService: ConfirmationService,
              private spinner: NgxSpinnerService,) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'roleCode', header: 'Role code' },
      { field: 'name', header: 'Name' },
    ];

    this.colsRolePermission = [
      { field: 'isView', header: 'View' },
      { field: 'isAdd', header: 'Add' },
      { field: 'isEdit', header: 'Edit' },
      { field: 'isDelete', header: 'Delete' },
      { field: 'isApprove', header: 'Approve' },
      { field: 'isStatistic', header: 'Statistic' },
    ];
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

    this.roleService.getListData(this.getListPagingRequest).subscribe({
      next: (data) => {
        this.tableData = data.data;
        this.totalRecords = data.totalRow;
      },
      error: (err) => this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
    });

    // Delete data on Form
    this.formGroup = createDefaultRoleForm();
  }

  showViewDialog() {
    this.formGroup = createDefaultRoleForm();
    let id = this.getIdSelections('view')[0];
    if(!id)
      return;
    this.roleService.getData(id).subscribe({
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

  showViewDialogRolePermission() {
    let selects: GetListRolePermissionRequestModel[] = [];
    if (Array.isArray(this.selectedListData) && this.selectedListData.length >= 1) {
      this.selectedListData.map(el => {
        selects.push({groupId: el.groupId, roleId: el.roleId});
      })
    }
    if(!(selects.length === 1)) {
      this.messageService.add({severity: 'error', summary: 'Error', detail: `Please select a row to set permission`, life: Enum.messageLife});
      return;
    }

    this.formGroupRolePermission = createDefaultRolePermissionForm();
    this.roleService.getListRolePermission(selects[0]).subscribe({
      next: (data) => {
        this.tableDataRolePermission = data.data;
        this.totalRecordsRolePermission = data.totalRow;
      },
      error: (err) => {
        this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
      }
    });

    this.visiblePermission = true;
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
    this.roleService.getData(id).subscribe({
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
    this.formGroup = createDefaultRoleForm();
  }

  saveData() {
    if(this.formGroup.invalid) return;
    this.spinner.show();
    if(this.isEdit)
      this.roleService.updateData(this.formGroup.value).subscribe({
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
      this.roleService.addData(this.formGroup.value).subscribe({
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

  saveDataRolePermission() {
    this.spinner.show();
    if(this.isEdit)
      this.roleService.updateData(this.formGroup.value).subscribe({
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
    this.visiblePermission = false;
  }

  deleteData(ids: string[]) {
    this.spinner.show();
    this.roleService.deleteData(ids).subscribe({
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
