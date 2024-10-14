import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {GetListRequestModel} from "../../models/base/get-list-request.model";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import {CourseTemplateModel} from "../../models/management/course-template.model";

@Injectable({
  providedIn: 'root'
})
export class CourseTemplateService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<CourseTemplateModel[]>>(this.baseUrl + "courseTemplate/get-list-paging", model);
  }

  getData(id: string) {
    return this.http.get<CourseTemplateModel>(this.baseUrl + `courseTemplate/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `courseTemplate`, JSON.stringify(model));
  }

  updateData(model: any) {
    return this.http.put<boolean>(this.baseUrl + `courseTemplate`, JSON.stringify(model));
  }

  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `courseTemplate/delete-list`, JSON.stringify(deleteListRequest));
  }
}
