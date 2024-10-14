import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { GetListRequestModel } from "../../models/base/get-list-request.model";
import { GetListResponseModel } from "../../models/base/get-list-response.model";
import { CourseModel } from "../../models/training/course.model";
import { SelectListItem } from "../../models/base/select-list-item.model";

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<CourseModel[]>>(this.baseUrl + "course/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<CourseModel>(this.baseUrl + `course/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `course`, JSON.stringify(model));
  }

  updateData(model: any) {
    return this.http.put<boolean>(this.baseUrl + `course`, JSON.stringify(model));
  }

  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `course/delete-list`, JSON.stringify(deleteListRequest));
  }
  getCombobox(model: any) {
    return this.http.post<SelectListItem[]>(this.baseUrl + "course/get-all-for-combobox", model);
  }
}
