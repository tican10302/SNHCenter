import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {AccountService} from "../../../services/system/account.service";
import {Permission} from "../../../models/system/permission";
import {NgFor, NgIf} from "@angular/common";
import {ButtonModule} from "primeng/button";
import {DialogModule} from "primeng/dialog";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {IconFieldModule} from "primeng/iconfield";
import {InputIconModule} from "primeng/inputicon";
import {InputTextModule} from "primeng/inputtext";
import {Table, TableModule} from "primeng/table";
import {TableColumn} from "../../../models/base/tableColumn";
import {CreateDefaultGetListPagingRequest, GetListPagingRequest} from "../../../models/base/getListPagingRequest";
import {ConfirmationService, MessageService} from "primeng/api";
import {Enum} from "../../../enums/enum";
import {ProgramService} from "../../../services/category/program.service";
import {createDefaultProgramForm, ProgramModel} from "../../../models/category/program/programModel";
import {ProgramViewComponent} from "./program-view/program-view.component";
import {createFormGroup} from "../../../models/base/ModelFormGroup";

@Component({
  selector: 'app-program',
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
    ProgramViewComponent,
    ReactiveFormsModule,
  ],
  templateUrl: './program.component.html',
  styleUrl: './program.component.scss'
})
export class ProgramComponent implements OnInit{
  @ViewChild('dataTable') dataTable!: Table;
  permission: Permission | null = null;
  formGroup = createDefaultProgramForm();
  visible: boolean = false;
  isView: boolean = false;
  isEdit: boolean = false;
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: ProgramModel[];
  selectedListData!: ProgramModel;
  cols!: TableColumn[];
  totalRecords: number = 0;

  getListPagingRequest: GetListPagingRequest = CreateDefaultGetListPagingRequest();


  constructor(protected accountService: AccountService,
              private programService: ProgramService,
              private messageService: MessageService,
              private confirmationService: ConfirmationService,) {
  }

  ngOnInit() {
    this.permission = this.accountService.getPermission(this.currentRoute || '');
    this.loadData(null);

    this.cols = [
      { field: 'name', header: 'Name' },
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
    if(event)
    {
      this.getListPagingRequest.offset = event.first;
      this.getListPagingRequest.limit = event.rows;
    }

    this.programService.getListData(this.getListPagingRequest).subscribe({
      next: (data) => {
        this.tableData = data.data;
        this.totalRecords = data.totalRow;
      },
      error: (err) => this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
    });
  }

  showViewDialog() {
    let id = this.getIdSelections('view')[0];
    if(!id)
      return;
    this.programService.getData(id).subscribe({
      next: (data) => {
        this.formGroup = createFormGroup(data);
      },
      error: (err) => {
        console.log(err)
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
    this.programService.getData(id).subscribe({
      next: (data) => {
        this.formGroup = createFormGroup(data);
      },
      error: (err) => {
        console.log(err)
        this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
      }
    });

    this.isView = false;
    this.isEdit = true;
    this.visible = true;
  }

  saveData() {
    if(this.formGroup.invalid) return;
    if(this.isEdit)
      this.programService.updateData(this.formGroup.value);
    else
      this.programService.addData(this.formGroup.value).subscribe({
        next: (data) => {
          if(data)
          {
            this.messageService.add({severity: 'success', summary: 'Success', detail: `Create data successfully`, life: Enum.messageLife});
          }
        },
        error: (err) =>  this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
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

  confirmDelete(event: Event) {
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
        this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'Record deleted' });
      },
      reject: () => {
        this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
      },
    });
  }

}
