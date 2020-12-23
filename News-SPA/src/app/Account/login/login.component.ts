import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
model:any={};
loginForm:FormGroup;
  constructor(private service:AuthService,private alertify:AlertifyService,private router:Router) { }

  ngOnInit(): void {
  }
  login(){
    this.service.login(this.model).subscribe(
      suc=>{this.alertify.success(' Login Success')},
      error=>{this.alertify.error(error)},
      ()=>{this.router.navigate(['/home']);}
    )
  }

}
