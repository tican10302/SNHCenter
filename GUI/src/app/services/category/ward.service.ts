import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { GetListPagingResponse } from "../../models/base/getListPagingResponse";
import { GetListPagingRequest } from "../../models/base/getListPagingRequest";
import { WardModel } from '../../models/category/ward/wardModel';

@Injectable({
  providedIn: 'root'
})
export class WardService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListPagingRequest) {
    return this.http.post<GetListPagingResponse<WardModel[]>>(this.baseUrl + "ward/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<WardModel>(this.baseUrl + `ward/${id}`);
  }
}
