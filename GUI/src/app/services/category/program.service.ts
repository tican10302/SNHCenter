import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {GetListPagingRequest} from "../../models/base/getListPagingRequest";
import {GetListPagingResponse} from "../../models/base/getListPagingResponse";
import {ProgramModel} from "../../models/category/program/programModel";

@Injectable({
  providedIn: 'root'
})
export class ProgramService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListPagingRequest) {
    return this.http.post<GetListPagingResponse<ProgramModel[]>>(this.baseUrl + "program/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<ProgramModel>(this.baseUrl + `program/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `program`, JSON.stringify(model));
  }

  updateData(model: any) {
    console.log(model)
    return this.http.put<boolean>(this.baseUrl + `program`, JSON.stringify(model));
  }

  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `program/delete-list`, JSON.stringify(deleteListRequest));
  }
}
