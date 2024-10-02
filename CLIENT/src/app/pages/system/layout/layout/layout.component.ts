import {Component, OnInit} from '@angular/core';
import {RouterOutlet} from "@angular/router";
import {HeaderComponent} from "../header/header.component";
import {FooterComponent} from "../footer/footer.component";
import {setTheme} from "ngx-bootstrap/utils";
import {SidebarComponent} from "../sidebar/sidebar.component";
import {AccountService} from "../../../../services/system/account.service";
import {User} from "../../../../models/system/user";
import {Account} from "../../../../models/system/account";
import {Menu} from "../../../../models/system/menu";
import {GroupPermission} from "../../../../models/system/groupPermission";
import {Permission} from "../../../../models/system/permission";
import { PrimeNGConfig } from 'primeng/api';
import { Aura } from 'primeng/themes/aura';
import {ToastModule} from "primeng/toast";
import { ConfirmDialogModule } from 'primeng/confirmdialog';

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
    const account: Account = JSON.parse(accountObj);
    const menu: Menu[] = menuList ? JSON.parse(menuList) : [];
    const permission: Permission[] = permissionList ? JSON.parse(permissionList) : [];
    const groupPermission: GroupPermission[] = groupPermissionList ? JSON.parse(groupPermissionList) : [];
    const user: User = {
      account,
      menu,
      permission,
      groupPermission,
      token: account.token || '',
    };
    this.accountService.setCurrentUser(user);
  }
}
