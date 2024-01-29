import { Component } from '@angular/core';
import {MatTableModule} from '@angular/material/table'; 
import { LeaveService } from '../../services/leave.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-leave-status',
  standalone: true,
  imports: [MatTableModule, DatePipe],
  templateUrl: './leave-status.component.html',
  styleUrl: './leave-status.component.css'
})
export class LeaveStatusComponent {
  dataSource : any = []
  displayedColumns: string[] = ['Leave Type', 'From - To', 'Date Submitted', 'Date Resolved', 'Status'];
  emp_id : number = Number(sessionStorage.getItem('userId'))
  constructor(private leaveService:LeaveService){}
  ngOnInit(){
    this.leaveService.getLeaveStatus(this.emp_id)
    .subscribe({
      next: (res) => {
        this.dataSource = res.sort((a:any,b:any)=> b.requestId - a.requestId)
      }
    })
  }
}
