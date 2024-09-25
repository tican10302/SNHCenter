import { Routes } from '@angular/router';
import {LoginComponent} from "./pages/system/login/login.component";
import {LayoutComponent} from "./pages/system/layout/layout.component";
import {DashboardComponent} from "./pages/system/dashboard/dashboard.component";

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
      }
    ]
  },
  {
    path: '**',
    redirectTo: '',
  }
];
