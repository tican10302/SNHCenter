import {Component, OnInit} from '@angular/core';
import {RouterOutlet} from "@angular/router";
import {HeaderComponent} from "../header/header.component";
import {FooterComponent} from "../footer/footer.component";
import {setTheme} from "ngx-bootstrap/utils";
import {SidebarComponent} from "../sidebar/sidebar.component";
import {AccountService} from "../../../../services/system/account.service";
import {UserModel} from "../../../../models/system/user.model";
import {AccountModel} from "../../../../models/system/account.model";
import {MenuModel} from "../../../../models/system/menu.model";
import {PermissionModel} from "../../../../models/system/permission.model";
import { PrimeNGConfig } from 'primeng/api';
import { Aura } from 'primeng/themes/aura';
import {ToastModule} from "primeng/toast";
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import {GroupPermissionModel} from "../../../../models/system/group-permission.model";

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    ToastModule,
    ConfirmDialogModule
  ],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent implements OnInit{
  constructor(private accountService: AccountService, private config: PrimeNGConfig) {
    setTheme('bs5');
    this.config.theme.set({ preset: Aura });
  }

  ngOnInit() {
    var tokenStr = localStorage.getItem('token');
    var accountObj = localStorage.getItem('user');
    var menuList = localStorage.getItem('menu');
    var permissionList = localStorage.getItem('permission');
    var groupPermissionList = localStorage.getItem('groupPermission');

    if(!tokenStr || !accountObj) return;
    const account: AccountModel = JSON.parse(accountObj);
    const menu: MenuModel[] = menuList ? JSON.parse(menuList) : [];
    const permission: PermissionModel[] = permissionList ? JSON.parse(permissionList) : [];
    const groupPermission: GroupPermissionModel[] = groupPermissionList ? JSON.parse(groupPermissionList) : [];
    const user: UserModel = {
      account,
      menu,
      permission,
      groupPermission,
      token: account.token || '',
    };
    this.accountService.setCurrentUser(user);
  }
}
