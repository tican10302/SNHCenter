import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import {GetListRequestModel} from "../../models/base/get-list-request.model";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import { LevelModel } from "../../models/category/level.model";
import { SelectListItem } from "../../models/base/select-list-item.model";

@Injectable({
  providedIn: 'root'
})
export class LevelService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<LevelModel[]>>(this.baseUrl + "level/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<LevelModel>(this.baseUrl + `level/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `level`, JSON.stringify(model));
  }

  updateData(model: any) {
    console.log(model)
    return this.http.put<boolean>(this.baseUrl + `level`, JSON.stringify(model));
  }

  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `level/delete-list`, JSON.stringify(deleteListRequest));
  }
  getCombobox(model: any) {
    return this.http.post<SelectListItem[]>(this.baseUrl + "level/get-all-for-combobox", model);
  }
}
