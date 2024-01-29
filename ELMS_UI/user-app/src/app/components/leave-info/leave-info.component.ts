import { Component } from '@angular/core';
import { LeaveService } from '../../services/leave.service';
import {MatTableModule} from '@angular/material/table'; 
@Component({
  selector: 'app-leave-info',
  standalone: true,
  imports: [MatTableModule],
  templateUrl: './leave-info.component.html',
  styleUrl: './leave-info.component.css'
})
export class LeaveInfoComponent {
  dataSource : any = []
  displayedColumns: string[] = ['Leave Type', 'Balances'];
  emp_id : number = Number(sessionStorage.getItem('userId'));

  constructor(private leaveService:LeaveService){}
  ngOnInit(){
    this.leaveService.getLeaveInfo(this.emp_id)
    .subscribe({
      next: (res) => {
        console.log(res)
        this.dataSource = res.sort((a:any,b:any)=> b.requestId - a.requestId)
      }
    })
  }
}
