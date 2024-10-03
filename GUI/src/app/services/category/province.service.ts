import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {GetListPagingResponse} from "../../models/base/getListPagingResponse";
import {GetListPagingRequest} from "../../models/base/getListPagingRequest";
import { ProvinceModel } from '../../models/category/province/provinceModel';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListPagingRequest) {
    return this.http.post<GetListPagingResponse<ProvinceModel[]>>(this.baseUrl + "province/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<ProvinceModel>(this.baseUrl + `province/${id}`);
  }
}
