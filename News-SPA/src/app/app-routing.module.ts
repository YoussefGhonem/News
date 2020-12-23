import { UpdateNewsComponent } from './News/update-news/update-news.component';
import { GetNewsComponent } from './News/get-news/get-news.component';
import { NewsDetailsResolver } from './_resolvers/news-details.resolver';
import { NewsListResolver } from './_resolvers/news-list.resolver';
import { ListNewsComponent } from './News/list-news/list-news.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './Account/login/login.component';
import { RegisterComponent } from './Account/register/register.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path:'',component:HomeComponent,resolve:{news:NewsListResolver}},
  {path:'home',component:HomeComponent,resolve:{news:NewsListResolver}},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'news/:id',component:GetNewsComponent,resolve:{news:NewsDetailsResolver}},
  {path:'updatenews/:id',component:UpdateNewsComponent,resolve:{news:NewsDetailsResolver}},





  { path: '**', redirectTo :'',pathMatch:'full'},// في حاله لو شخص بيكتب روابط غريبه

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
