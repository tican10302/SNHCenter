import {Component, OnInit} from '@angular/core';
import {ButtonModule} from "primeng/button";
import {Title} from "@angular/platform-browser";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    ButtonModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  constructor(private titleService: Title) {
  }

  ngOnInit() {
    this.titleService.setTitle('SNHCenter | Dashboard');
  }
}
