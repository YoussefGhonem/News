import { AlertifyService } from './../../_services/alertify.service';
import { Pagination, PaginationResult } from './../../_models/Pagination';
import { News } from './../../_models/News';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NewsService } from 'src/app/_services/News.service';

@Component({
  selector: 'app-list-news',
  templateUrl: './list-news.component.html',
  styleUrls: ['./list-news.component.css']
})
export class ListNewsComponent implements OnInit {
news:News[];
pagination:Pagination;
search:boolean=false;
  constructor(private route:ActivatedRoute,private service:NewsService,private alertify:AlertifyService) { }

  ngOnInit() {
    this.search=false;
    this.route.data.subscribe(
      data=>{this.news=data['news'].result;
            this.pagination=data['news'].pagination;
       });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }


  loadUsers(){
    if (!this.search) {
      this.pagination.currentPage=1;
       }
    this.service.getAllNews(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe((res: PaginationResult<News[]>) => {
      this.news = res.result;
      this.pagination = res.pagination;
  }, error => this.alertify.error(error))
}
}

