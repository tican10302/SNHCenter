import { Component } from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {AccountService} from "../../../../services/system/account.service";
import {AsyncPipe, NgIf} from "@angular/common";
import {FaIconComponent} from "@fortawesome/angular-fontawesome";
import {faCaretDown, faMagnifyingGlass, faToggleOn} from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    AsyncPipe,
    NgIf,
    FaIconComponent
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(protected accountService: AccountService, private router: Router) {
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  protected readonly faMagnifyingGlass = faMagnifyingGlass;
  protected readonly faCaretDown = faCaretDown;
  protected readonly faToggleOn = faToggleOn;
}
