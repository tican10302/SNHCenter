import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {GetListRequestModel} from "../../models/base/get-list-request.model";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import {ProgramModel} from "../../models/category/program.model";

@Injectable({
  providedIn: 'root'
})
export class ProgramService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<ProgramModel[]>>(this.baseUrl + "program/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<ProgramModel>(this.baseUrl + `program/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `program`, JSON.stringify(model));
  }

  updateData(model: any) {
    return this.http.put<boolean>(this.baseUrl + `program`, JSON.stringify(model));
  }

  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `program/delete-list`, JSON.stringify(deleteListRequest));
  }
}
