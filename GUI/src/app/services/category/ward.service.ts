import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import {GetListRequestModel} from "../../models/base/get-list-request.model";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import { WardModel } from "../../models/category/ward.model";
import { SelectListItem } from "../../models/base/select-list-item.model";


@Injectable({
  providedIn: 'root'
})
export class WardService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<WardModel[]>>(this.baseUrl + "ward/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<WardModel>(this.baseUrl + `ward/${id}`);
  }
  getCombobox(model: any) {
    return this.http.post<SelectListItem[]>(this.baseUrl + "ward/get-all-for-combobox", model);
  }
}
