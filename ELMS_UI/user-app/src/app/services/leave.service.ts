import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_API_URL } from '../constants/app-constants';
import { User } from '../models/user.model';
@Injectable({
  providedIn: 'root'
})
export class LeaveService {

  constructor(private http:HttpClient) { }

  getLeaveTypes() : Observable<User[]>{
    return this.http.get<User[]>(BASE_API_URL+'api/');
  }
}
