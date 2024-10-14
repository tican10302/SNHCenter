import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { GetListRequestModel } from "../../models/base/get-list-request.model";
import { GetListResponseModel } from "../../models/base/get-list-response.model";
import { ParentModel } from "../../models/management/parent.model";
import { SelectListItem } from "../../models/base/select-list-item.model";

@Injectable({
  providedIn: 'root'
})
export class ParentService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<ParentModel[]>>(this.baseUrl + "parent/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<ParentModel>(this.baseUrl + `parent/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `parent`, JSON.stringify(model));
  }

  updateData(model: any) {
    return this.http.put<boolean>(this.baseUrl + `parent`, JSON.stringify(model));
  }

  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `parent/delete-list`, JSON.stringify(deleteListRequest));
  }
  getCombobox(model: any) {
    return this.http.post<SelectListItem[]>(this.baseUrl + "parent/get-all-for-combobox", model);
  }

}
