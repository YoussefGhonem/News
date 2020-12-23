import { map } from 'rxjs/operators';
import { PaginationResult } from './../_models/Pagination';
import { Photo } from './../_models/Photo';
import { News } from './../_models/News';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { param } from 'jquery';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  NewsUrl = environment.baseURL + 'news/';
  PhotoUrl = environment.baseURL + 'photos/';
  constructor(private http: HttpClient) { }

  getNews(id: number): Observable<News> {
    return this.http.get<News>(this.NewsUrl + id);
  }

  // parameters : these Query String (URL الحجات اللي بتتكتب ف ال ) options
  //PaginationResult<News[]> : because i'm Return paginationResult;
  getAllNews(pageNumber?, pageSize?): Observable<PaginationResult<News[]>> {
    // paginationResult : To Take data (Result , Pagination) From API ;
    const paginationResult:PaginationResult<News[]> = new PaginationResult<News[]>();
    let params=new HttpParams();// Query Stringنرسل داتا من خلال ال 
    if(pageNumber != null &&  pageSize!=null){
      //'pageNumber' and 'pageSize' get Them From Postman Header API
      params=params.append('pageNumber',pageNumber);
      params=params.append('pageSize',pageSize);
    }

     // observe : default is 'body' but we using 'response' to get Response Header From API
    return this.http.get<News[]>(this.NewsUrl,{observe:'response',params}).pipe(
      // pip:Headerفكدا بتعامل مع ال  response علشان لما استخدمت bodyعلشان اعرف اوصل لل 
      map(    // data From Body Api
        result=>{
          paginationResult.result=result.body;
          if(result.headers.get('Pagination')!=null) //Pagination: Header postmanموجوده داخل ال
         {
           paginationResult.pagination=JSON.parse(result.headers.get('Pagination'));
         }
         return paginationResult;
        }
      )
    )
  }



  addNews(model:News){
    return this.http.post(this.NewsUrl,model);
  }
  updateNews(model:News,id:number){
    return this.http.put(this.NewsUrl+id,model);
  }

  //photos
  deletePhoto(NewsId,id:number){
    return this.http.delete(this.PhotoUrl+NewsId+'/delete/'+id);
  }
  setMainPhoto(NewsId,id:number){
    return this.http.post(this.PhotoUrl+NewsId+'/'+id+'/setMain',{});
  }

}
