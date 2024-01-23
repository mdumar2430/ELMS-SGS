import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../user';
import { Observable } from 'rxjs';
import {Buffer} from 'buffer';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  public isLoggedIn : boolean = false;
  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]>{
    return this.http.get<User[]>('https://localhost:44334/api/Users',{ responseType: 'json' })
  }

  userLogin(body:any):Observable<boolean>{
    const buffer = Buffer.from(body.Password, 'utf-8');
    var user={"Email":body.Email,"Password":buffer.toString('base64')}
    return this.http.post<boolean>('https://localhost:7034/api/Login', user)
  }
}
