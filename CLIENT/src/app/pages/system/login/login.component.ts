import {Component, inject, ViewEncapsulation} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {User} from "../../../models/user";
import {AccountService} from "../../../services/account.service";
import {BusyService} from "../../../services/busy.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginObj = {
    "UserName": "",
    "Password": "",
  }
  model: any = {};

  constructor(private accountService: AccountService, private router: Router) {
  }

  login() {
    this.accountService.login(this.loginObj).subscribe({
      next: (_) => {
        this.router.navigateByUrl('/');
      },
      error: (err) => console.log(err.error),
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
