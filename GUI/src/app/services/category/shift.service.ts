import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { GetListPagingRequest } from "../../models/base/getListPagingRequest";
import { GetListPagingResponse } from "../../models/base/getListPagingResponse";
import { ShiftModel } from "../../models/category/shift/shiftModel";

@Injectable({
  providedIn: 'root'
})
export class ShiftService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListPagingRequest) {
    return this.http.post<GetListPagingResponse<ShiftModel[]>>(this.baseUrl + "shift/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<ShiftModel>(this.baseUrl + `shift/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `shift`, JSON.stringify(model));
  }

  updateData(model: any) {
    console.log(model)
    return this.http.put<boolean>(this.baseUrl + `shift`, JSON.stringify(model));
  }

  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `shift/delete-list`, JSON.stringify(deleteListRequest));
  }
}
