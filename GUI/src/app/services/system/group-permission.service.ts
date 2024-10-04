import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {GetListRequestModel} from "../../models/base/get-list-request.model";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import {GroupPermissionModel} from "../../models/system/group-permission.model";
import {SelectListItem} from "../../models/base/select-list-item.model";

@Injectable({
  providedIn: 'root'
})
export class GroupPermissionService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<GroupPermissionModel[]>>(this.baseUrl + "grouppermission/get-list", model);
  }

  getData(id: string) {
    return this.http.get<GroupPermissionModel>(this.baseUrl + `grouppermission/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `grouppermission`, JSON.stringify(model));
  }

  updateData(model: any) {
    return this.http.put<boolean>(this.baseUrl + `grouppermission`, JSON.stringify(model));
  }

  getCombobox(model: any) {
    return this.http.post<SelectListItem[]>(this.baseUrl + "grouppermission/get-all-for-combobox", model);
  }
}
