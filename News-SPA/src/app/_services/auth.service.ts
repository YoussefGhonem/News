import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { from, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseURL = environment.baseURL + 'auth/';
  constructor(private http: HttpClient) { }
  decodedToken: any;
  jwtHelper = new JwtHelperService();// علشان يعرف هل التوكين ده صح ولا لا
  currentUser: User;

  
  login(model: any) {
    return this.http.post(this.baseURL + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) { 
          localStorage.setItem('token', user.token);
          localStorage.setItem('user',JSON.stringify(user.user))
      this.decodedToken=this.jwtHelper.decodeToken(user.token);
      this.currentUser = user.user;
     
      
      }
      }))
  }

  register(user: User) {
    return this.http.post(this.baseURL + 'register', user);
  }

  logesdIn(){
    try {
      const token = localStorage.getItem('token');
      return !this.jwtHelper.isTokenExpired(token);
    }
    catch {
      return false;
    }

  }

  roleMatch(AllowedRoles:Array<string>) : boolean{
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    AllowedRoles.forEach(element =>{
      if(userRoles.includes(element)){
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }
}
