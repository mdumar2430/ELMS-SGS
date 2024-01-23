import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_API_URL } from '../constants/app-constants';
import { User } from '../models/user.model';
import { LeaveType } from '../models/leave-type.model';
@Injectable({
  providedIn: 'root'
})
export class LeaveService {

  constructor(private http:HttpClient) { }

  getLeaveTypes() : Observable<LeaveType[]>{
    return this.http.get<LeaveType[]>(BASE_API_URL+'api/LeaveTypes/GetLeaveTypeName');
  }
}
