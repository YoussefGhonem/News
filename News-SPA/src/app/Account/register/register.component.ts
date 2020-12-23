import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/_models/User';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
user:User;
registerForm:FormGroup;
  constructor(private fp:FormBuilder,private service:AuthService,private alertify:AlertifyService,private router:Router) { }

  ngOnInit(): void {
    this.CreateRegisterForm();
  }
  CreateRegisterForm(){
   this.registerForm=  this.fp.group({
    username:['',Validators.required],
       email:['',[Validators.email,Validators.required]],
       password:['',Validators.required]
     })
  }
  register(){
    if(this.registerForm.valid){
      this.user=Object.assign({},this.registerForm.value);
      this.service.register(this.user).subscribe(
        succ=>{this.alertify.success("Regester Is Done Please Login")
        this.router.navigate(['/login']);
      },
        err=>{this.alertify.error(err)}
      )
    }

  }

}
