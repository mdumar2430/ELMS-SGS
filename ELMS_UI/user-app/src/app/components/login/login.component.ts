import { Component } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { Router } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { UserService } from "../../services/user.service";
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatIconModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers:[HttpClientModule]
})

export class LoginComponent {

    userEmail: string = "";
    userPass: string = "";
    btnClicked: boolean = false;

    constructor(
      private _router: Router
      ,private userService: UserService
      ,private _snackBar: MatSnackBar
      ) { }

    ngOnInit(){
      this.userService.isLoggedIn = false
    }

    login() {
      var payload = { "Email": this.userEmail, "Password": this.userPass };
      this.userService.userLogin(payload)
      .subscribe(
      {
          next: (res)=>{
            if(res){
              this.userService.isLoggedIn = true
              this._snackBar.open('Login Successfull', 'Ok', {
                duration: 2000
              });
              this._router.navigate(['/users'])
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

    logout() {
        this.btnClicked = false;
    }
}
