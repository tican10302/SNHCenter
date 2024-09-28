import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {NgFor, NgIf} from "@angular/common";
import {Title} from "@angular/platform-browser";
import {AccountService} from "../../../services/account.service";
import {Permission} from "../../../models/permission";
import {ActivatedRoute} from "@angular/router";
import {Table, TableModule} from "primeng/table";
import {ProvinceService} from "../../../services/category/province.service";
import {Province} from "../../../models/category/models/province";
import {TableColumn} from "../../../models/base/tableColumn";
import {GetListPagingRequest} from "../../../models/base/getListPagingRequest";
import {MessageService} from "primeng/api";
import {IconFieldModule} from "primeng/iconfield";
import {InputIconModule} from "primeng/inputicon";
import {ButtonModule} from "primeng/button";
import {DialogModule} from "primeng/dialog";
import {InputTextModule} from "primeng/inputtext";

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
    InputTextModule
  ],
  templateUrl: './province.component.html',
  styleUrl: './province.component.scss'
})
export class ProvinceComponent implements OnInit{
  @ViewChild('dataTable') dataTable!: Table;
  permission: Permission | null = null;
  visible: boolean = true;
  isEdit: boolean = false;
  currentRoute = inject(ActivatedRoute).routeConfig?.component?.name.replace(/_?([a-zA-Z]+)Component$/, '$1').toLowerCase() || '';

  // Table
  tableData!: Province[];
  selectedDatas!: Province;
  cols!: TableColumn[];
  totalRecords: number = 0;

  model: GetListPagingRequest = {
    search: '',
    fromDate: null,
    toDate: null,
    offset: 0,
    limit: 10,
    order: null,
    sort: null,
  };

  constructor(private titleService: Title,
              protected accountService: AccountService,
              private provinceService: ProvinceService,
              private messageService: MessageService,) {
  }

  ngOnInit() {
    this.titleService.setTitle('SNHCenter | Province');
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
    this.model.search = input.value;
    this.model.offset = 0;
    this.loadData(null);
  }

  loadData(event: any | null) {
    if(event)
    {
      this.model.offset = event.first;
      this.model.limit = event.rows;
    }

    this.provinceService.getData(this.model).subscribe(data => {

      this.tableData = data.data;
      this.totalRecords = data.totalRow;

    }, error => {
      this.messageService.add({severity: 'error', summary: 'Error', detail: error.message, life: 5000});
    })
  }

  showItems() {
    console.log(this.getIdSelections('view'));
  }

  getIdSelections(action: string, multi: boolean = false) {
    if (Array.isArray(this.selectedDatas) && this.selectedDatas.length === 1) {
      return this.selectedDatas[0].code;
    } else {
      // Hiển thị cảnh báo cho người dùng nếu không đúng 1 mục được chọn
      this.messageService.add({severity: 'error', summary: 'Error', detail: `Please select a row to ${action}`, life: 5000});
    }
  }
}
