import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {ButtonModule} from "primeng/button";
import {DialogModule} from "primeng/dialog";
import {IconFieldModule} from "primeng/iconfield";
import {InputIconModule} from "primeng/inputicon";
import {InputTextModule} from "primeng/inputtext";
import {NgClass, NgFor, NgIf} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MessageService, SharedModule} from "primeng/api";
import {Table, TableModule} from "primeng/table";
import {TextareaModule} from "primeng/textarea";
import {PermissionModel} from "../../../models/system/permission.model";
import {ActivatedRoute} from "@angular/router";
import {TableColumnModel} from "../../../models/base/table-column.model";
import {GetListRequestModel} from "../../../models/base/get-list-request.model";
import {AccountService} from "../../../services/system/account.service";
import {NgxSpinnerService} from "ngx-spinner";
import {Enum} from "../../../enums/enum";
import {createFormGroup} from "../../../models/base/form-group.model";
import {createDefaultMenuForm, GetListMenuRequestModel, MenuModel} from "../../../models/system/menu.model";
import {MenuService} from "../../../services/system/menu.service";
import {DropdownModule} from "primeng/dropdown";
import {SysConfig} from "../../../models/base/sys-config.model";
import {GroupPermissionService} from "../../../services/system/group-permission.service";
import {SelectListItem} from "../../../models/base/select-list-item.model";
import {InputNumberModule} from "primeng/inputnumber";
import {Select} from "primeng/select";
import {ToggleSwitchModule} from "primeng/toggleswitch";
import {CheckboxModule} from "primeng/checkbox";
import {InputSwitchModule} from "primeng/inputswitch";

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    ButtonModule,
    DialogModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    NgFor,
    NgIf,
    ReactiveFormsModule,
    SharedModule,
    TableModule,
    TextareaModule,
    DropdownModule,
    InputNumberModule,
    NgClass,
    Select,
    ToggleSwitchModule,
    CheckboxModule,
    InputSwitchModule,
    FormsModule
  ],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent implements OnInit{
  @ViewChild('dataTable') dataTable!: Table;
  permission: PermissionModel | null = null;
  formGroup = createDefaultMenuForm();
  visible: boolean = false;
  isView: boolean = false;
  isEdit: boolean = false;
  activeCombobox = SysConfig.IsActive;
  groupPermissionCombobox: SelectListItem[] = [];
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: MenuModel[];
  selectedListData!: MenuModel;
  cols!: TableColumnModel[];
  totalRecords: number = 0;

  getListPagingRequest = new GetListMenuRequestModel();


  constructor(protected accountService: AccountService,
              private menuService: MenuService,
              private messageService: MessageService,
              private spinner: NgxSpinnerService,
              private groupPermissionService: GroupPermissionService,) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'name', header: 'Name' },
      { field: 'groupName', header: 'Group Permission' },
      { field: 'hasView', header: 'View', class: 'text-center' },
      { field: 'hasAdd', header: 'Add', class: 'text-center' },
      { field: 'hasEdit', header: 'Edit', class: 'text-center' },
      { field: 'hasDelete', header: 'Delete', class: 'text-center' },
      { field: 'hasApprove', header: 'Approve', class: 'text-center' },
      { field: 'hasStatistic', header: 'Statistic', class: 'text-center' },
      { field: 'isActive', header: 'Status', class: 'text-center' },
    ];

    this.groupPermissionService.getCombobox({}).subscribe({
      next: (data) => {
        this.groupPermissionCombobox = [{ text: '-- Group permission --', value: null }, ...data];
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

    this.menuService.getListData(this.getListPagingRequest).subscribe({
      next: (data) => {
        this.tableData = data.data;
        this.totalRecords = data.totalRow;
      },
      error: (err) => this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
    });

    // Delete data on Form
    this.formGroup = createDefaultMenuForm();
  }

  showViewDialog() {
    let id = this.getIdSelections('view')[0];
    if(!id)
      return;
    this.menuService.getData(id).subscribe({
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
    this.formGroup = createDefaultMenuForm();
    this.isView = false;
    this.isEdit = false;
    this.visible = true;
  }

  showEditDialog() {
    let id = this.getIdSelections('view')[0];
    if(!id)
      return;
    this.menuService.getData(id).subscribe({
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
    this.formGroup = createDefaultMenuForm();
  }

  saveData() {
    if(this.formGroup.invalid) return;
    this.spinner.show();
    if(this.isEdit)
      this.menuService.updateData(this.formGroup.value).subscribe({
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
      this.menuService.addData(this.formGroup.value).subscribe({
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

  onGroupPermissionChange(selectedValue: any) {
    this.getListPagingRequest.groupPermissionId = selectedValue;
    this.loadData(null);
  }
}
