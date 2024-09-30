import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {GetListPagingRequest} from "../../models/base/getListPagingRequest";
import {GetListPagingResponse} from "../../models/base/getListPagingResponse";
import {Program} from "../../models/category/program/models/program";

@Injectable({
  providedIn: 'root'
})
export class ProgramService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListPagingRequest) {
    return this.http.post<GetListPagingResponse<Program[]>>(this.baseUrl + "program/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<Program>(this.baseUrl + `program/${id}`);
  }

  addData(model: Program) {
    const header = new HttpHeaders()
      .set('Content-type', 'application/json');
    const body = JSON.stringify(model);
    console.log(body);
    return this.http.post<boolean>(this.baseUrl + `program`, body, {headers: header});
  }

  updateData(model: Program) {
    return this.http.put<boolean>(this.baseUrl + `program/${model.id}`, model);
  }
}
