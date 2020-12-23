import { Component, Input, OnInit } from '@angular/core';
import { News } from 'src/app/_models/News';

@Component({
  selector: 'app-card-list-news',
  templateUrl: './card-list-news.component.html',
  styleUrls: ['./card-list-news.component.css']
})
export class CardListNewsComponent implements OnInit {
@Input()tidings:News;
  constructor() { }

  ngOnInit(): void {
  }

}
