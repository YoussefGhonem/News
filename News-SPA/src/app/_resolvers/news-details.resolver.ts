import { AlertifyService } from "../_services/alertify.service";
import {  ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { NewsService } from './../_services/News.service';
import { News } from './../_models/News';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class NewsDetailsResolver  implements Resolve<News>{

    constructor(private newsService:NewsService,private alertify:AlertifyService,private route:Router) {}
    resolve(route:ActivatedRouteSnapshot): Observable<News>{
        return this.newsService.getNews(route.params['id']).pipe(
            catchError(
                err=>{
                    this.alertify.error('Can not load News List');
                    //this.route.navigate[''];
                    return of(null);
                }
            )
        )
    }
}
