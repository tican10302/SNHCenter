import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {GetListRequestModel} from "../../models/base/get-list-request.model";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import {MenuModel} from "../../models/system/menu.model";
import {SelectListItem} from "../../models/base/select-list-item.model";
import {RoleModel} from "../../models/system/role.model";
import {GetListRolePermissionRequestModel, RolePermissionModel} from "../../models/system/role-permission.model";

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<RoleModel[]>>(this.baseUrl + "role/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<RoleModel>(this.baseUrl + `role/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `role`, JSON.stringify(model));
  }

  updateData(model: any) {
    return this.http.put<boolean>(this.baseUrl + `role`, JSON.stringify(model));
  }

  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `role/delete-list`, JSON.stringify(deleteListRequest));
  }

  getCombobox(model: any) {
    return this.http.post<SelectListItem[]>(this.baseUrl + "role/get-all-for-combobox", model);
  }

  getListRolePermission(model: GetListRolePermissionRequestModel) {
    return this.http.post<RolePermissionModel[]>(this.baseUrl + "role/get-list-role-permission", JSON.stringify(model));
  }

  postRolePermission(model: any) {
    return this.http.post<RolePermissionModel>(this.baseUrl + `role/post-role-permission`, JSON.stringify(model));
  }
}
