import {Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {AccountService} from "../../../services/account.service";
import {Router} from "@angular/router";
import {NgxSpinnerService} from "ngx-spinner";
import {MessageService, PrimeNGConfig} from "primeng/api";
import {ToastModule} from "primeng/toast";
import {Aura} from "primeng/themes/aura";
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    ToastModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{
  loginObj = {
    "UserName": "",
    "Password": "",
  }
  model: any = {};

  constructor(
    private accountService: AccountService,
    private router: Router,
    private messageService: MessageService,
    private spinner: NgxSpinnerService,
    private config: PrimeNGConfig,
    private titleService: Title) {
    this.config.theme.set({ preset: Aura });
  }

  ngOnInit() {
    this.titleService.setTitle('SNHCenter | Sign in');
  }

  login() {
    this.spinner.show();
    this.accountService.login(this.loginObj).subscribe({
      next: (_) => {
        this.spinner.hide();
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Log in successfully!', life: 5000 });
        window.setTimeout(() => {
          this.router.navigateByUrl('dashboard');
        }, 1000);
      },
      error: (err) => {
        this.spinner.hide();
        console.log(err);
        if(err.error) {
          if(err.error.status === 500)
          {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Internal server error', life: 5000 });
          }
          else {
            this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: 5000});
          }
        }
        else
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Cannot connect to the server', life: 5000 });
      },
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
