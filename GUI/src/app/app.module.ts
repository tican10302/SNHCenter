import { NgModule } from '@angular/core';
import {TooltipModule} from "ngx-bootstrap/tooltip";
import { GroupPermissionComponent } from './pages/system/group-permission/group-permission.component';
import { MenuComponent } from './pages/system/menu/menu.component';
import { RoleComponent } from './pages/system/role/role.component';

@NgModule({
  declarations: [
  
    GroupPermissionComponent,
       MenuComponent,
       RoleComponent
  ],
  imports: [
    TooltipModule.forRoot()
  ],
})
export class AppModule { }
