import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { GetListRequestModel } from "../../models/base/get-list-request.model";
import { GetListResponseModel } from "../../models/base/get-list-response.model";
import { CourseModel } from "../../models/training/coursemodel";

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  // Fetch paginated list of courses
  getListData(model: GetListRequestModel) {
    return this.http.post<GetListResponseModel<CourseModel[]>>(this.baseUrl + "course/get-list-paging", model);
  }

  // Get course details by ID
  getData(id: string) {
    return this.http.get<CourseModel>(this.baseUrl + `course/${id}`);
  }

  // Add a new course
  addData(model: any) {
    return this.http.post<boolean>(this.baseUrl + `course`, JSON.stringify(model));
  }

  // Update an existing course
  updateData(model: any) {
    return this.http.put<boolean>(this.baseUrl + `course`, JSON.stringify(model));
  }

  // Delete courses by ID array
  deleteData(model: string[]) {
    let deleteListRequest = {
      ids: model
    };
    return this.http.post<boolean>(this.baseUrl + `course/delete-list`, JSON.stringify(deleteListRequest));
  }
}
