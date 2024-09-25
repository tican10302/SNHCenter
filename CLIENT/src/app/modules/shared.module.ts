import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ToastrModule, ToastrService} from "ngx-toastr";
import {BrowserModule} from "@angular/platform-browser";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {NgxSpinnerModule} from "ngx-spinner";
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {loadingInterceptor} from "../interceptors/loading.interceptor";
import {AccountService} from "../services/account.service";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    }),
    NgxSpinnerModule.forRoot({
      type: 'timer',
    }),
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useValue: loadingInterceptor, multi: true }],
  exports: [ToastrModule, NgxSpinnerModule]
})
export class SharedModule { }
