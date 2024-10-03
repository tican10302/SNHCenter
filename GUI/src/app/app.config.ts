import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import {provideRouter, TitleStrategy} from '@angular/router';

import { routes } from './app.routes';
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {authInterceptor} from "./interceptors/auth.interceptor";
import {provideToastr} from "ngx-toastr";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";
import {ConfirmationService, MessageService} from "primeng/api";
import {TemplatePageTitleStrategy} from "./extensions/TemplatePageTitleStrategy";

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([authInterceptor])),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    {provide: TitleStrategy, useClass: TemplatePageTitleStrategy},
    provideAnimationsAsync(),
    MessageService,
    provideToastr({
      positionClass: 'toast-bottom-right',
    }),
    ConfirmationService
  ],
};


