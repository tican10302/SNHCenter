import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {GetListPagingResponse} from "../../models/base/getListPagingResponse";
import {Province} from "../../models/category/models/province";

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getData(model: any) {
    return this.http.post<GetListPagingResponse<Province[]>>(this.baseUrl + "province/get-list-paging", model);
  }
}
