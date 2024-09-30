import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {GetListPagingResponse} from "../../models/base/getListPagingResponse";
import {Province} from "../../models/category/province/models/province";
import {GetListPagingRequest} from "../../models/base/getListPagingRequest";

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListPagingRequest) {
    return this.http.post<GetListPagingResponse<Province[]>>(this.baseUrl + "province/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<Province>(this.baseUrl + `province/${id}`);
  }
}
