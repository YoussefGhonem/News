import { AlertifyService } from "../_services/alertify.service";
import {  Resolve, Router } from '@angular/router';
import { NewsService } from './../_services/News.service';
import { News } from './../_models/News';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class NewsListResolver  implements Resolve<News[]>{
    pageNumber = 1;
    pageSize = 6;
    constructor(private newsService:NewsService,private alertify:AlertifyService,private route:Router) {}
   
    resolve(): Observable<News[]>{
        return this.newsService.getAllNews(this.pageNumber,this.pageSize).pipe(
            catchError(
                err=>{
                    this.alertify.error('Can not load News List');
                    this.route.navigate[''];
                    return of(null);
                }
            )
        )
    }
}
