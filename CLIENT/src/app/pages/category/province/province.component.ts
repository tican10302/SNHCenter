import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {NgFor, NgIf} from "@angular/common"
import {AccountService} from "../../../services/system/account.service";
import {Permission} from "../../../models/system/models/permission";
import {ActivatedRoute} from "@angular/router";
import {Table, TableModule} from "primeng/table";
import {ProvinceService} from "../../../services/category/province.service";
import {createDefaultProvince, Province} from "../../../models/category/province/models/province";
import {TableColumn} from "../../../models/base/tableColumn";
import {CreateDefaultGetListPagingRequest, GetListPagingRequest} from "../../../models/base/getListPagingRequest";
import {MessageService} from "primeng/api";
import {IconFieldModule} from "primeng/iconfield";
import {InputIconModule} from "primeng/inputicon";
import {ButtonModule} from "primeng/button";
import {DialogModule} from "primeng/dialog";
import {InputTextModule} from "primeng/inputtext";
import {Enum} from "../../../enums/enum";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-province',
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
  ],
  templateUrl: './province.component.html',
  styleUrl: './province.component.scss'
})
export class ProvinceComponent implements OnInit{
  @ViewChild('dataTable') dataTable!: Table;
  permission: Permission | null = null;
  visible: boolean = false;
  isEdit: boolean = false;
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: Province[];
  selectedListData!: Province;
  cols!: TableColumn[];
  totalRecords: number = 0;

  getListPagingRequest: GetListPagingRequest = CreateDefaultGetListPagingRequest();

  // Data
  model: Province = createDefaultProvince();

  constructor(protected accountService: AccountService,
              private provinceService: ProvinceService,
              private messageService: MessageService,) {
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
    if(event)
    {
      this.getListPagingRequest.offset = event.first;
      this.getListPagingRequest.limit = event.rows;
    }

    this.provinceService.getListData(this.getListPagingRequest).subscribe({
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
    this.provinceService.getData(id).subscribe({
      next: (data) => {
        this.model = data;
      },
      error: (err) => {
        this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife})
      }
    });

    this.isEdit = false;
    this.visible = true;
  }

  saveData() {
    this.isEdit = false;
    this.visible = false;
  }

  getIdSelections(action: string, multi: boolean = false) {
    let selects: string[] = [];
    if (Array.isArray(this.selectedListData) && this.selectedListData.length >= 1) {
      selects = this.selectedListData.map(el => {
        return el.code;
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
}
