import { AlertifyService } from './../../_services/alertify.service';
import { NewsService } from './../../_services/News.service';
import { News } from './../../_models/News';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { data } from 'jquery';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-update-news',
  templateUrl: './update-news.component.html',
  styleUrls: ['./update-news.component.css']
})
export class UpdateNewsComponent implements OnInit {
  news: News;
  @ViewChild('updatForm') updatForm:NgForm;
  constructor(private route: ActivatedRoute,private service:NewsService,private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.route.data.subscribe(
      data => { this.news = data['news'] });
  }
  update() {
    this.service.updateNews(this.news,this.news.id).subscribe(
      ()=>{
        this.alertify.success('Updated Is Done');
        this.updatForm.reset(this.news);
      },
      err=>{this.alertify.error(err);}
    )
  }
}
