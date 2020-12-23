import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { News } from 'src/app/_models/News';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-get-news',
  templateUrl: './get-news.component.html',
  styleUrls: ['./get-news.component.css']
})
export class GetNewsComponent implements OnInit {
news:News;
galleryOptions: NgxGalleryOptions[];
galleryImages: NgxGalleryImage[];
  constructor(private route:ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(
      data=>{this.news=data['news']} )
        //Gallary
        this.galleryOptions=[{
          width:'600px',height:'500px',imagePercent:100,thumbnailsColumns:4,
          imageAnimation:NgxGalleryAnimation.Slide,preview:false
        }]
    
        this.galleryImages=this.getImages();
  }
  getImages(){
    const imageUrls=[];
    for(let i =0;i<this.news.photos.length;i++){
      imageUrls.push({
        small:this.news.photos[i].url,
        medium:this.news.photos[i].url,
        big:this.news.photos[i].url,
      })
    };
    return imageUrls;
  }

}
