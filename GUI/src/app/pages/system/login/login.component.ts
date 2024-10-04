import {Component, OnInit} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AccountService} from "../../../services/system/account.service";
import {Router} from "@angular/router";
import {NgxSpinnerService} from "ngx-spinner";
import {MessageService, PrimeNGConfig} from "primeng/api";
import {ToastModule} from "primeng/toast";
import {Aura} from "primeng/themes/aura";
import {Enum} from "../../../enums/enum";
import {PasswordModule} from "primeng/password";
import {InputTextModule} from "primeng/inputtext";
import {NgIf} from "@angular/common";
import {FormGroupModel} from "../../../models/base/form-group.model";
import {createDefaultLoginForm, LoginModel} from "../../../models/system/login.model";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    ToastModule,
    PasswordModule,
    ReactiveFormsModule,
    InputTextModule,
    NgIf
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{
  formView!: FormGroupModel<LoginModel>;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private messageService: MessageService,
    private spinner: NgxSpinnerService,
    private config: PrimeNGConfig,) {
    this.config.theme.set({ preset: Aura });
  }

  ngOnInit() {
    this.formView = createDefaultLoginForm();
  }

  login() {
    if(this.formView.invalid) return;
    this.spinner.show();
    this.accountService.login(this.formView.value).subscribe({
      next: (_) => {
        this.spinner.hide();
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Log in successfully!', life: Enum.messageLife });
        window.setTimeout(() => {
          this.router.navigateByUrl('dashboard');
        }, 1000);
      },
      error: (err) => {
        this.spinner.hide();
        if(err.error) {
          if(err.error.status === 500)
          {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Internal server error', life: Enum.messageLife });
          }
          else {
            this.messageService.add({severity: 'error', summary: 'Error', detail: err.error.message, life: Enum.messageLife});
          }
        }
        else
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Cannot connect to the server', life: Enum.messageLife });
      },
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
