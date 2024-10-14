import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {GetListResponseModel} from "../../models/base/get-list-response.model";
import {GetListLessonTemplateRequestModel, LessonTemplateModel} from "../../models/management/lesson-template.model";
import {GetListRequestModel} from "../../models/base/get-list-request.model";

@Injectable({
  providedIn: 'root'
})
export class LessonTemplateService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<LessonTemplateModel[]>>(this.baseUrl + "lessonTemplate/get-list-paging", model);
  }

  getAllData(courseTemplateId: string) {
    return this.http.post<LessonTemplateModel[]>(this.baseUrl + "lessonTemplate/get-all", courseTemplateId);
  }

  getData(id: string) {
    return this.http.get<LessonTemplateModel>(this.baseUrl + `lessonTemplate/${id}`);
  }

  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `lessonTemplate`, JSON.stringify(model));
  }

  updateData(model: any) {
    return this.http.put<boolean>(this.baseUrl + `lessonTemplate`, JSON.stringify(model));
  }

  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `lessonTemplate/delete-list`, JSON.stringify(deleteListRequest));
  }
}
