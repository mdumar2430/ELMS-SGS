import { Component } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatNativeDateModule, NativeDateAdapter} from '@angular/material/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormControl, Validators, ReactiveFormsModule, FormsModule, FormGroup} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import { LeaveService } from '../../services/leave.service';
import { LeaveType } from '../../models/leave-type.model';
import { DatePipe } from '@angular/common';
import { LeaveRequest } from '../../models/leave-request.model';


interface Animal {
  name: string;
  sound: string;
}

@Component({
  selector: 'app-leave-request',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatIconModule,MatFormFieldModule,
     MatInputModule, MatButtonModule, ReactiveFormsModule, MatSelectModule,MatDatepickerModule, MatNativeDateModule ],
  templateUrl: './leave-request.component.html',
  styleUrl: './leave-request.component.css',
  providers: [NativeDateAdapter,DatePipe],
})
export class LeaveRequestComponent {
  leaveTypeControl = new FormControl<LeaveType | null>(null, Validators.required);
  reason = new FormControl('', Validators.required);
  leaveTypes:LeaveType[] = []
  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  constructor(
    private leaveService:LeaveService
    ,private datePipe:DatePipe
    ,private _snackBar: MatSnackBar
    ){

  }

  ngOnInit(){
    this.getLeaveTypes()
  }

  getLeaveTypes(){
    this.leaveService.getLeaveTypes()
    .subscribe(
      {
        next:(res)=>{
          this.leaveTypes = res;
        }
      }
    )
  }

  sendRequest(){
    var payLoad : LeaveRequest = new LeaveRequest()
    payLoad.EmployeeId = Number(sessionStorage.getItem('userId'))
    payLoad.LeaveTypeId = this.leaveTypeControl.value?.leaveTypeId
    payLoad.StartDate = this.range.controls.start.value?.toISOString();
    payLoad.EndDate = this.range.controls.end.value?.toISOString();
    payLoad.dateSubmitted = new Date(Date.now()).toISOString()
    payLoad.Comments = this.reason.value
    this.leaveService.postLeaveRequest(payLoad)
    .subscribe({
      next : (res) => {
        this._snackBar.open('Form Submitted Successfully', 'Ok', {
          duration:2000
        });
        this.leaveTypeControl.reset()
        this.reason.reset()
        this.range.reset()
      },
      error: (err)=>{
        this._snackBar.open(err.error, 'Ok', {
          duration: 2000
        });
    }
    })    
  }
  
}
