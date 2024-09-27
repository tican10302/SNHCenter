import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {BehaviorSubject, map} from "rxjs";
import {User} from "../models/user";
import {HttpClient} from "@angular/common/http";
import {Account} from "../models/account";
import {Menu} from "../models/menu";
import {GroupPermission} from "../models/groupPermission";
import {Permission} from "../models/permission";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<Account | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  private currentMenuSource = new BehaviorSubject<Menu[] | null>(null);
  currentMenu$ = this.currentMenuSource.asObservable();
  private currentPermissionSource = new BehaviorSubject<Permission[] | null>(null);
  currentPermission$ = this.currentPermissionSource.asObservable();
  private currentGroupPermissionSource = new BehaviorSubject<GroupPermission[] | null>(null);
  currentGroupPermission$ = this.currentGroupPermissionSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + "account/login", model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + "account/register", model).pipe(
      map((response: User) => {
        const user = response;
        if(user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('token', JSON.stringify(user.token));
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

}
