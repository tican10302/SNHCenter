import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { GetListPagingResponse } from "../../models/base/getListPagingResponse";
import { GetListPagingRequest } from "../../models/base/getListPagingRequest";
import { DistrictModel } from '../../models/category/district/districtModel';

@Injectable({
  providedIn: 'root'
})
export class DistrictService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListPagingRequest) {
    return this.http.post<GetListPagingResponse<DistrictModel[]>>(this.baseUrl + "district/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<DistrictModel>(this.baseUrl + `district/${id}`);
  }
}
