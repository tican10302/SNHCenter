import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {NgClass, NgFor, NgIf} from "@angular/common";
import {Table, TableModule} from "primeng/table";
import {IconFieldModule} from "primeng/iconfield";
import {InputIconModule} from "primeng/inputicon";
import {ButtonModule} from "primeng/button";
import {DialogModule} from "primeng/dialog";
import {InputTextModule} from "primeng/inputtext";
import {FormControl, FormsModule, ReactiveFormsModule} from "@angular/forms";
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
import {TabPanel, Tabs, TabsChangeEvent} from "primeng/tabs";
import {GroupPermissionModel} from "../../../models/system/group-permission.model";
import {GroupPermissionService} from "../../../services/system/group-permission.service";
import {of, switchMap, throwError} from "rxjs";
import {InputSwitchModule} from "primeng/inputswitch";
import {CheckboxModule} from "primeng/checkbox";

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
    NgClass,
    InputSwitchModule,
    CheckboxModule,
  ],
  templateUrl: './role.component.html',
  styleUrl: './role.component.scss'
})
export class RoleComponent implements OnInit{
  @ViewChild('dataTable') dataTable!: Table;
  permission: PermissionModel | null = null;
  formGroup = createDefaultRoleForm();
  visible: boolean = false;
  visiblePermission: boolean = false;
  isView: boolean = false;
  isEdit: boolean = false;
  tabsRolePermission: GroupPermissionModel[] = [];
  currentTableData = [];
  groupId: string | null = null;
  roleId: string | null = null;
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: RoleModel[];
  selectedListData!: RoleModel;
  cols!: TableColumnModel[];
  totalRecords: number = 0;

  // Table Role Permission
  tableDataRolePermission!: RolePermissionModel[];
  formGroupRolePermission = createDefaultRolePermissionForm();
  colsRolePermission!: TableColumnModel[];

  getListPagingRequest = new GetListRequestModel();


  constructor(protected accountService: AccountService,
              private roleService: RoleService,
              private messageService: MessageService,
              private confirmationService: ConfirmationService,
              private spinner: NgxSpinnerService,
              private groupPermissionService: GroupPermissionService) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'roleCode', header: 'Role code' },
      { field: 'name', header: 'Name' },
    ];

    this.colsRolePermission = [
      { field: 'name', header: 'Name' },
      { field: 'isView', header: 'View', class: 'text-center', options: 'hasView' },
      { field: 'isAdd', header: 'Add', class: 'text-center', options: 'hasAdd' },
      { field: 'isEdit', header: 'Edit', class: 'text-center', options: 'hasEdit' },
      { field: 'isDelete', header: 'Delete', class: 'text-center', options: 'hasDelete' },
      { field: 'isApprove', header: 'Approve', class: 'text-center', options: 'hasApprove' },
      { field: 'isStatistic', header: 'Statistic', class: 'text-center', options: 'hasStatistic' },
      { field: 'hasView', header: 'View', class: 'text-center', visible: true },
      { field: 'hasAdd', header: 'Add', class: 'text-center', visible: true },
      { field: 'hasEdit', header: 'Edit', class: 'text-center', visible: true },
      { field: 'hasDelete', header: 'Delete', class: 'text-center', visible: true },
      { field: 'hasApprove', header: 'Approve', class: 'text-center', visible: true },
      { field: 'hasStatistic', header: 'Statistic', class: 'text-center', visible: true },
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
    this.spinner.show();
    let selects: GetListRolePermissionRequestModel[] = [];

    //Get List Group Permission
    this.groupPermissionService.getAllData().pipe(
      switchMap((data) => {
        this.tabsRolePermission = data;

        // Get Role Permission
        if (Array.isArray(this.selectedListData) && this.selectedListData.length >= 1) {

          const selects = this.selectedListData.map(el => ({ groupId: this.tabsRolePermission[0].id, roleId: el.id }));

          if (selects.length === 1) {
            this.groupId = selects[0].groupId;
            this.roleId = selects[0].roleId;
            return of(selects);
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: `Please select a row to set permission`,
              life: Enum.messageLife
            });
            return throwError(() => new Error('More than one row selected.'));
          }
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: `No rows selected`,
            life: Enum.messageLife
          });
          return throwError(() => new Error('No rows selected.'));
        }
      })
    ).subscribe({
      next: data => {
        this.visiblePermission = true;
        this.groupId = data[0].groupId;
        this.loadTableData();
      },
      error:err => {
        this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
      }
    })
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

  closeDialogRolePermission() {
    this.visiblePermission = false;
    this.isEdit = false;
    this.visible = false;
    this.formGroupRolePermission = createDefaultRolePermissionForm();
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

  // Load tab set permission
  onTabChange(event: any) {
    this.spinner.show();
    const selectedTabIndex = event.index;
    const selectedTab = this.tabsRolePermission[selectedTabIndex];
    this.groupId = selectedTab.id;
    this.loadTableData();
  }

  loadTableData() {
    let request = new GetListRolePermissionRequestModel();
    request.roleId = this.roleId;
    request.groupId = this.groupId;

    this.roleService.getListRolePermission(request).subscribe({
      next: data => {
        this.tableDataRolePermission = data;
        this.spinner.hide();
      },
      error: err => {
        this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife});
        this.spinner.hide();
      }
    })
  }

  setPermission(id: any) {
    console.log(id);
  }
}
