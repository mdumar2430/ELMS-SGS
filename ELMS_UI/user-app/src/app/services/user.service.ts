import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Buffer} from 'buffer';
import { User } from '../models/user.model';
import { BASE_API_URL } from '../constants/app-constants';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  public isLoggedIn : boolean = false;
  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]>{
    return this.http.get<User[]>('https://localhost:44334/api/Users',{ responseType: 'json' })
  }

  userLogin(body:any):Observable<{ token: string }>{
    const buffer = Buffer.from(body.Password, 'utf-8');
    var user={"Email":body.Email,"Password":buffer.toString('base64')}
    return this.http.post<{ token: string }>('https://localhost:7034/api/Login', user, {responseType: 'json'})
  }

  isUserLoggedIn(){
    return sessionStorage.getItem('isLoggedIn');
  }

  getManagerId(payload:number):Observable<number>{
    return this.http.post<number>(BASE_API_URL+'api/Managers/GetManagerIdByEmployeeId', payload)
  }
}
