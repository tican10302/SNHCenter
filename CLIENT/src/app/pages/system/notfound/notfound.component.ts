import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {NgOptimizedImage} from "@angular/common";
import {Title} from "@angular/platform-browser";

@Component({
  selector: 'app-notfound',
  standalone: true,
  imports: [
    RouterLink,
    NgOptimizedImage
  ],
  templateUrl: './notfound.component.html',
  styleUrl: './notfound.component.scss'
})
export class NotfoundComponent implements OnInit{
  constructor(private titleService: Title) {
  }
  ngOnInit() {
    this.titleService.setTitle("SNHCenter | Not found");
  }
}
