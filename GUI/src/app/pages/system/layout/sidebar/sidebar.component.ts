import { Component } from '@angular/core';
import {Router, RouterLink, RouterLinkActive} from "@angular/router";
import {FaIconComponent} from "@fortawesome/angular-fontawesome";
import {AccountService} from "../../../../services/system/account.service";
import {AsyncPipe, NgClass, NgFor, NgIf} from "@angular/common";
import * as icons from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    RouterLink,
    FaIconComponent,
    AsyncPipe,
    NgFor,
    NgIf,
    RouterLinkActive,
    NgClass
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  icons = icons;
  constructor(protected accountService: AccountService, private router: Router) {

  }

  createSlug(name: string | null): string {
    if (!name) return '';

    return name
      .toLowerCase()
      .replace(/\s+/g, '-')
      .replace(/[^\w\-]+/g, '');
  }

  getIcon(iconName: string | null): any {
    if (iconName && (iconName in this.icons)) {
      return this.icons[iconName as keyof typeof icons];
    } else {
      return this.icons.faListUl;
    }
  }

  hasActiveSubmenu(menu: any): boolean {
    const currentMenu = this.accountService.currentMenuSource.value;
    let isActive: boolean = false;
    const currentUrl = this.router.url.split('/').pop();

    if (currentMenu) {
      const filteredSubmenu = currentMenu.filter((submenu: any) => {
        return submenu.groupPermissionId === menu.id && submenu.isActive && submenu.controller === currentUrl;
      });
      isActive = filteredSubmenu.length > 0;
    }

    return isActive;
  }
}
