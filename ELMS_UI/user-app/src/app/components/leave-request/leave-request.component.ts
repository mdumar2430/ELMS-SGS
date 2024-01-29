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
    start: new FormControl<Date | null>(null, [Validators.required]),
    end: new FormControl<Date | null>(null, [Validators.required]),
  });
  minDate = new Date()
  

  constructor(
    private leaveService:LeaveService
    ,private datePipe:DatePipe
    ,private _snackBar: MatSnackBar
    ){

  }

  myFilter = (d: Date | null): boolean => {
    const day = (d || new Date()).getDay();
    // Prevent Saturday and Sunday from being selected.
    return day !== 0 && day !== 6;
  };

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

    const start_date = new Date(this.range.controls.start.value || '');
    var year = start_date.getFullYear();
    var month = String(start_date.getMonth() + 1).padStart(2, '0');
    var day = String(start_date.getDate()).padStart(2, '0');
    payLoad.StartDate = `${year}-${month}-${day}`;

    const end_date = new Date(this.range.controls.end.value || '');
    year = end_date.getFullYear();
    month = String(end_date.getMonth() + 1).padStart(2, '0');
    day = String(end_date.getDate()).padStart(2, '0');
    payLoad.EndDate = `${year}-${month}-${day}`;

    const submitted_date = new Date();
    year = submitted_date.getFullYear();
    month = String(submitted_date.getMonth() + 1).padStart(2, '0');
    day = String(submitted_date.getDate()).padStart(2, '0');
    payLoad.dateSubmitted = `${year}-${month}-${day}`

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
