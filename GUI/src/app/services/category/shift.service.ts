import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { GetListRequestModel } from "../../models/base/get-list-request.model";
import { GetListResponseModel } from "../../models/base/get-list-response.model";
import { ShiftModel } from "../../models/category/shift.model";

@Injectable({
  providedIn: 'root'
})
export class ShiftService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<ShiftModel[]>>(this.baseUrl + "shift/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<ShiftModel>(this.baseUrl + `shift/${id}`);
  }

  addData(model: any) {
    console.log(model)
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
