import {Component, inject, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {AccountService} from "../../../services/account.service";
import {Permission} from "../../../models/permission";
import {find} from "rxjs";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-program',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './program.component.html',
  styleUrl: './program.component.scss'
})
export class ProgramComponent implements OnInit{
  route = inject(ActivatedRoute);
  accountService = inject(AccountService);
  currentComponentName: string = '';
  permission: Permission | null = null;
  constructor() {
  }
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.currentComponentName = data['componentName'] || '';
    })

    this.accountService.currentPermission$.subscribe(data => {
      if (data) {
        const permission = data.find(x => x.controllerName === this.currentComponentName);
        this.permission = permission || null;
      } else {
        this.permission = null;
      }
    });
  }
}
