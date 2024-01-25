import { Component } from '@angular/core';
import { LeaveService } from '../../services/leave.service';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatNativeDateModule, NativeDateAdapter} from '@angular/material/core';
import { PendingLeaveRequest } from '../../models/pending-leave-request';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-pending-requests',
  standalone: true,
  imports: [MatExpansionModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,MatNativeDateModule, CommonModule],
  templateUrl: './pending-requests.component.html',
  styleUrl: './pending-requests.component.css',
  providers:[NativeDateAdapter]
})
export class PendingRequestsComponent {
  managerID : number = Number(sessionStorage.getItem('managerId'))
  pendingList : any[] = []
  step = 0;

  setStep(index: number) {
    this.step = index;
  }

  nextStep() {
    this.step++;
  }

  prevStep() {
    this.step--;
  }
  constructor(private leaveService:LeaveService){

  }

  ngOnInit(){
    this.leaveService.getPendingLeaveRequest(this.managerID)
    .subscribe({
      next: (res) => {
        this.pendingList = res.sort((a,b) => b.leaveRequest.requestId - a.leaveRequest.requestId)
      }
    })
  }
}
