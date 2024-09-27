import { Routes } from '@angular/router';
import {LoginComponent} from "./pages/system/login/login.component";
import {LayoutComponent} from "./pages/system/layout/layout/layout.component";
import {DashboardComponent} from "./pages/system/dashboard/dashboard.component";
import {authGuard} from "./guards/auth.guard";
import {ProvinceComponent} from "./pages/category/province/province.component";
import {loginGuard} from "./guards/login.guard";
import {AppComponent} from "./app.component";
import {NotfoundComponent} from "./pages/system/notfound/notfound.component";

export const routes: Routes = [
  {
    path: '',
    component: AppComponent,
    canActivate: [loginGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { componentName: 'Login' }
  },
  {
    path: '',
    component: LayoutComponent,
    data: { componentName: 'Layout' },
    canActivate: [authGuard],
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
        data: { componentName: 'Dashboard' }
      },
      {
        path: 'province',
        component: ProvinceComponent,
        data: { componentName: 'Province' }
      }
    ]
  },
  {
    path: 'notfound',
    component: NotfoundComponent
  },
  {
    path: '**',
    redirectTo: 'notfound',
  }
];
