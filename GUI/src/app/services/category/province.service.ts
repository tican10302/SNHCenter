import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import {GetListRequestModel} from "../../models/base/get-list-request.model";
import { ProvinceModel } from '../../models/category/province.model';
import { SelectListItem } from "../../models/base/select-list-item.model";


@Injectable({
  providedIn: 'root'
})
export class ProvinceService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<ProvinceModel[]>>(this.baseUrl + "province/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<ProvinceModel>(this.baseUrl + `province/${id}`);
  }
  getCombobox(model: any) {
    return this.http.post<SelectListItem[]>(this.baseUrl + "province/get-all-for-combobox", model);
  }
}
