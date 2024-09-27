import { Component } from '@angular/core';
import {RouterLink, RouterLinkActive} from "@angular/router";
import {FaIconComponent} from "@fortawesome/angular-fontawesome";
import {faHouse, faListUl} from "@fortawesome/free-solid-svg-icons";
import {AccountService} from "../../../../services/account.service";
import {AsyncPipe, NgFor, NgIf} from "@angular/common";

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    RouterLink,
    FaIconComponent,
    AsyncPipe,
    NgFor,
    NgIf,
    RouterLinkActive
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  protected readonly faHouse = faHouse;
  constructor(protected accountService: AccountService) {
  }

  protected readonly faListUl = faListUl;

  createSlug(name: string | null): string {
    if (!name) return '';

    return name
      .toLowerCase()
      .replace(/\s+/g, '-')
      .replace(/[^\w\-]+/g, '');
  }
}
