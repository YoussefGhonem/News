import { Pagination } from './_models/Pagination';
import { HasRoleDirective } from './_directives/has-role.directive';
import { NewsDetailsResolver } from './_resolvers/news-details.resolver';
import { LimitWords } from './_directives/limit-words.directive';
import { NewsListResolver } from './_resolvers/news-list.resolver';
import { NewsService } from './_services/News.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FileUploadModule} from 'ng2-file-upload';
import { PaginationModule } from 'ngx-bootstrap/pagination';
//Services 
import{AuthService} from './_services/auth.service';
import{AlertifyService}from'./_services/alertify.service';
//Component
import { AppComponent } from './app.component';
import { from } from 'rxjs';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './Account/login/login.component';
import { RegisterComponent } from './Account/register/register.component';
import { HomeComponent } from './home/home.component';
import { NaveComponent } from './nave/nave.component';
import { FooterComponent } from './footer/footer.component';
import { AddNewsComponent } from './News/add-news/add-news.component';
import { UpdateNewsComponent } from './News/update-news/update-news.component';
import { GetNewsComponent } from './News/get-news/get-news.component';
import { ListNewsComponent } from './News/list-news/list-news.component';
import { CardListNewsComponent } from './News/card-list-news/card-list-news.component';
import { PhotoEditorComponent } from './News/photo-editor/photo-editor.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// JWT Setting // هنا علشان ابعت التوكين لكل ريكويست من الدوت نت 
export function tokenGetter() {
  return localStorage.getItem('token');
}
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    NaveComponent,
    FooterComponent,
    AddNewsComponent,
    UpdateNewsComponent,
    GetNewsComponent,
    ListNewsComponent,
    CardListNewsComponent,
    LimitWords ,
    HasRoleDirective,
    PhotoEditorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
    ReactiveFormsModule, //Reactive Validation
    JwtModule.forRoot({ // JWT  // هنا علشان ابعت التوكين لكل ريكويست من الدوت نت 
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/auth']// هنا مش هياخد التوكين معاه علشان مش هحتاجه ف التسجيل او اللوجين
      }
    }),
    
    HttpClientModule,
    NgxGalleryModule,
    FileUploadModule,
    BrowserAnimationsModule
  ],
  providers: [  // Services , Guards , Resolvers
    AuthService,
    AlertifyService,
    NewsService,
    NewsListResolver,
    NewsDetailsResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
