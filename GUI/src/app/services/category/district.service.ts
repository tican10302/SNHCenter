import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import {GetListRequestModel} from "../../models/base/get-list-request.model";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import { DistrictModel } from "../../models/category/district.model";
import { SelectListItem } from "../../models/base/select-list-item.model";

@Injectable({
  providedIn: 'root'
})
export class DistrictService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<DistrictModel[]>>(this.baseUrl + "district/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<DistrictModel>(this.baseUrl + `district/${id}`);
  }
  getCombobox(model: any) {
    return this.http.post<SelectListItem[]>(this.baseUrl + "district/get-all-for-combobox", model);
  }
}
