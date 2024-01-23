import { Component } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { Router } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { UserService } from "../../services/user.service";
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormControl, Validators, ReactiveFormsModule} from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatIconModule,MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers:[HttpClientModule]
})

export class LoginComponent {

    email = new FormControl('', [Validators.required, Validators.email]);
    password = new FormControl('', [Validators.required, Validators.minLength(8)]);
    hide = true;

    constructor( 
      private _router: Router
      ,private userService: UserService
      ,private _snackBar: MatSnackBar
      ) { }

    ngOnInit(){
      this.userService.isLoggedIn = false
    }

    login() {
      var payload = { "Email": this.email.value, "Password": this.password.value };
      this.userService.userLogin(payload)
      .subscribe(
      {
          next: (res)=>{
            if(res){
              this.userService.isLoggedIn = true
              this._snackBar.open('Login Successfull', 'Ok', {
                duration: 2000
              });
              this._router.navigate(['/leave-request'])
            }
            else{
              this._snackBar.open('Login Failed - Invalid Email or Password', 'Ok', {
                duration: 2000
              });
            }
          },
          error: (err)=>{
            this._snackBar.open('Login Failed - Invalid Email or Password', 'Ok', {
              duration: 2000
            });
        }
      }
      )
    }
    getEmailErrorMessage() {
      if (this.email.hasError('required')) {
        return 'You must enter a value';
      }
  
      return this.email.hasError('email') ? 'Not a valid email' : '';
    }

    getPasswordErrorMessage() {
      if (this.password.hasError('required')) {
        return 'You must enter a value';
      }
  
      return this.password.hasError('minlength') ? 'Password must contain 8 characters.' : '';
    }
}
