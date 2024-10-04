import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {GetListRequestModel} from "../../models/base/get-list-request.model";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import {MenuModel} from "../../models/system/menu.model";

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<MenuModel[]>>(this.baseUrl + "menu/get-list", model);
  }

  getAllData() {
    return this.http.get<GetListResponseModel<MenuModel[]>>(this.baseUrl + "menu/get-all");
  }

  getData(id: string) {
    return this.http.get<MenuModel>(this.baseUrl + `menu/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `menu`, JSON.stringify(model));
  }

  updateData(model: any) {
    return this.http.put<boolean>(this.baseUrl + `menu`, JSON.stringify(model));
  }
}
