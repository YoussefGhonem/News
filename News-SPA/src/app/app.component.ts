import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from 'src/app/_models/User';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private auth: AuthService) { }
  jwtHelperService = new JwtHelperService();
  ngOnInit(): void {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token)
      this.auth.decodedToken = this.jwtHelperService.decodeToken(token);
    if (user)
      this.auth.currentUser = user;

  }

}
