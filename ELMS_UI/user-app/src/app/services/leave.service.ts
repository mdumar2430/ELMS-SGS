import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BASE_API_URL } from '../constants/app-constants';
import { User } from '../models/user.model';
import { LeaveType } from '../models/leave-type.model';
import { LeaveRequest } from '../models/leave-request.model';
import { PendingLeaveRequest } from '../models/pending-leave-request';
@Injectable({
  providedIn: 'root'
})
export class LeaveService {
  noOfPendingLeaveRequests : number = 0;
  
  constructor(private http:HttpClient) { }
  token=sessionStorage.getItem("jwt");
  
  getLeaveTypes() : Observable<LeaveType[]>{
    return this.http.get<LeaveType[]>(BASE_API_URL+'api/LeaveTypes/GetLeaveTypeName');
  }

  postLeaveRequest(payLoad:LeaveRequest) : Observable<boolean>{
    return this.http.post<boolean>(BASE_API_URL+'api/LeaveRequests/AddLeaveRequest', payLoad)
  }

  getPendingLeaveRequest(payload:number): Observable<PendingLeaveRequest[]>{
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.token}`,
    });

    // Add headers to the request options
    const options = { headers };

    // Make the HTTP request with headers
    return this.http.post<PendingLeaveRequest[]>(
      BASE_API_URL + 'api/LeaveRequests/GetPendingLeaveRequest',
      payload,
      options
    );
    
  }

  putApproveLeaveRequest(payload:number):Observable<boolean>{
    return this.http.put<boolean>(BASE_API_URL+'api/LeaveRequests/ApproveLeaveRequest', payload);
  }

  putDenyLeaveRequest(payload:number):Observable<boolean>{
    return this.http.put<boolean>(BASE_API_URL+'api/LeaveRequests/DenyLeaveRequest', payload);
  }

}
