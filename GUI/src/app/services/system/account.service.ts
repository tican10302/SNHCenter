import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {BehaviorSubject, map} from "rxjs";
import {UserModel} from "../../models/system/user.model";
import {HttpClient} from "@angular/common/http";
import {AccountModel} from "../../models/system/account.model";
import {MenuModel} from "../../models/system/menu.model";
import {PermissionModel} from "../../models/system/permission.model";
import {GroupPermissionModel} from "../../models/system/group-permission.model";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<AccountModel | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  public currentMenuSource = new BehaviorSubject<MenuModel[] | null>(null);
  currentMenu$ = this.currentMenuSource.asObservable();
  private currentPermissionSource = new BehaviorSubject<PermissionModel[] | null>(null);
  currentPermission$ = this.currentPermissionSource.asObservable();
  private currentGroupPermissionSource = new BehaviorSubject<GroupPermissionModel[] | null>(null);
  currentGroupPermission$ = this.currentGroupPermissionSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<UserModel>(this.baseUrl + "account/login", model).pipe(
      map((response: UserModel) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<UserModel>(this.baseUrl + "account/register", model).pipe(
      map((response: UserModel) => {
        const user = response;
        if(user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: UserModel) {
    localStorage.setItem('token', user.token);
    localStorage.setItem('user', JSON.stringify(user.account));
    localStorage.setItem('menu', JSON.stringify(user.menu));
    localStorage.setItem('permission', JSON.stringify(user.permission));
    localStorage.setItem('groupPermission', JSON.stringify(user.groupPermission));
    this.currentUserSource.next(user.account);
    this.currentMenuSource.next(user.menu);
    this.currentPermissionSource.next(user.permission);
    this.currentGroupPermissionSource.next(user.groupPermission);
  }

  logout(): void {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    localStorage.removeItem('menu');
    localStorage.removeItem('permission');
    localStorage.removeItem('groupPermission');
    this.currentUserSource.next(null);
    this.currentMenuSource.next(null);
    this.currentPermissionSource.next(null);
    this.currentGroupPermissionSource.next(null);
  }

  getPermission(route: string): PermissionModel {
    var controller = route.toLowerCase().trim() + 'controller';
    var permissionCurrent: PermissionModel = {
      role: null,
      roleId: null,
      controllerName: controller,
      isView: false,
      isAdd: false,
      isEdit: false,
      isDelete: false,
      isApprove: false,
      isStatistic: false,
    };
    this.currentPermission$.pipe(
      map(permissions => {
        return permissions ? permissions.find(permission => permission.controllerName === controller) : null;
      })
    ).subscribe(foundPermission => {
      if (foundPermission) {
        permissionCurrent = foundPermission;
      }
    });

    return permissionCurrent;
  }

}
