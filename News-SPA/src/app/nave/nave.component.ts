import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nave',
  templateUrl: './nave.component.html',
  styleUrls: ['./nave.component.css']
})
export class NaveComponent implements OnInit {

  constructor(public auth: AuthService,private alertify:AlertifyService,private router :Router) { }

  ngOnInit(): void {
  }
  
  logedIn() {
    return this.auth.logesdIn();
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.alertify.message('تم الخروج');
    this.router.navigate(['/home']);
  }
}
