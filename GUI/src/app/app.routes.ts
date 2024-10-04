import { Routes } from '@angular/router';
import {LoginComponent} from "./pages/system/login/login.component";
import {LayoutComponent} from "./pages/system/layout/layout/layout.component";
import {DashboardComponent} from "./pages/system/dashboard/dashboard.component";
import {authGuard} from "./guards/auth.guard";
import {ProvinceComponent} from "./pages/category/province/province.component";
import {loginGuard} from "./guards/login.guard";
import {AppComponent} from "./app.component";
import {NotfoundComponent} from "./pages/system/notfound/notfound.component";
import {ProgramComponent} from "./pages/category/program/program.component";
import {GroupPermissionComponent} from "./pages/system/group-permission/group-permission.component";
import {MenuComponent} from "./pages/system/menu/menu.component";
import {RoleComponent} from "./pages/system/role/role.component";
import { DistrictComponent } from './pages/category/district/district.component';
import { WardComponent } from './pages/category/ward/ward.component';
import { LevelComponent } from './pages/category/level/level.component';
import { ShiftComponent } from './pages/category/shift/shift.component';

export const routes: Routes = [
  {
    path: '',
    component: AppComponent,
    canActivate: [loginGuard],
  },
  {
    path: 'login',
    title: 'Sign in',
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
        title: 'Dashboard',
        component: DashboardComponent,
        data: { componentName: 'Dashboard' }
      },
      {
        path: 'province',
        title: 'Province',
        component: ProvinceComponent,
        data: { componentName: 'Province' }
      },
      {
        path: 'district',
        title: 'District',
        component: DistrictComponent,
        data: { componentName: 'District' }
      },
      {
        path: 'ward',
        title: 'Ward',
        component: WardComponent,
        data: { componentName: 'Ward' }
      },
      {
        path: 'program',
        title: 'Program',
        component: ProgramComponent,
        data: { componentName: 'Program' }
      },
      {
        path: 'level',
        title: 'Level',
        component: LevelComponent,
        data: { componentName: 'Level' }
      },
      {
        path: 'shift',
        title: 'Shift',
        component: ShiftComponent,
        data: { componentName: 'Shift' }
      },
      {
        path: 'grouppermission',
        title: 'Group Permission',
        component: GroupPermissionComponent,
        data: { componentName: 'GroupPermission' }
      },
      {
        path: 'menu',
        title: 'Menu',
        component: MenuComponent,
        data: { componentName: 'Menu' }
      },
      {
        path: 'role',
        title: 'Role',
        component: RoleComponent,
        data: { componentName: 'Role' }
      },
    ]
  },
  {
    path: 'notfound',
    title: 'Not found',
    component: NotfoundComponent
  },
  {
    path: '**',
    redirectTo: 'notfound',
  }
];
