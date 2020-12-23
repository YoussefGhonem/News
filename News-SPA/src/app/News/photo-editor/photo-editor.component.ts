import { AlertifyService } from './../../_services/alertify.service';
import { NewsService } from './../../_services/News.service';
import { Photo } from './../../_models/Photo';
import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { News } from 'src/app/_models/News';
@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];
  @Input() newsId: News;
  baseUrl = environment.baseURL;
  currentMain :Photo;
  constructor(private service: NewsService, private alert: AlertifyService) { }
  fileOverBase(e: any): void // ng2-file-upload
  {
    this.hasBaseDropZoneOver = e;
  }
  uploader: FileUploader;// ng2-file-upload
  hasBaseDropZoneOver = false; // ng2-file-upload

  ngOnInit(): void {
    this.initializeUploader();
  }
  initializeUploader()// ng2-file-upload
  {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'Photos/' + this.newsId,
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],// نوع الملفات اللي هتترفع
      removeAfterUpload: true,// Remove photos  from Queue (UI) After finished Uploaded
      autoUpload: false,// رفع الصورة تلقائي من غي الضغط على الزر
      maxFileSize: 10 * 1024 * 1024 // حجم الملف اللي هيترفع
    });
    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; }; // Using This For CORS Pplicy Problems
    // ده علشان اول ما اعمل رفع للصورة تظهرلي على طول مع مجموعه الصور
    this.uploader.onSuccessItem = (item, Response, status, headers) => {
      if (Response) {
        //photo Return DTo
        const res: Photo = JSON.parse(Response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          isMain: res.isMain
        };
        this.photos.push(photo);
      }
    }
  }
  deletePhoto(id: number,photo:Photo) {
    if(photo.isMain===true){
      this.alert.error('Please Change Main photo Firstly ');
    }
    this.service.deletePhoto(this.newsId, id).subscribe(
      () => {
        // splice: //  تستخدم مع المصفوفات علشان تحدد البدايه والنهايه علشان نحذف عنصر معين او نستبدله
        //findIndex: // الخاص بالعنصر اللي اخترته داخل المصفوفه على حسب شرط معين  index بتجبلي 
        // انا هنا كتبت 1 علسان هو عنصر واحد فقط اللي هيتم حذفه
        this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
        this.alert.success("Deleted Is Done");
      },
      err => { this.alert.error(err); }

    )
  }
  setMainPhoto(photo:Photo){
    this.service.setMainPhoto(this.newsId,photo.id).subscribe(
      ()=>{
        this.currentMain=this.photos.filter(p=>p.isMain===true)[0];
        this.currentMain.isMain=false;
        photo.isMain=true
      },
      ()=>{this.alert.error('يوجد مشكلة في الصورة الأساسية');}
    )
  }
}
