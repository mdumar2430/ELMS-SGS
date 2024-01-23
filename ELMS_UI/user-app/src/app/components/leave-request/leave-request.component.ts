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
  reason = new FormControl(null, Validators.required);
  leaveTypes:LeaveType[] = []
  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  constructor(private leaveService:LeaveService,private datePipe:DatePipe){

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
    let leaveTypeId = this.leaveTypeControl.value?.leaveTypeId
    let start = this.range.controls.start.value;
    const formatStartDate:string|null=this.datePipe.transform(start,'yyyy-MM-dd');
    console.log(formatStartDate)
    let end = this.range.controls.end.value;
    const formatEndDate:string|null=this.datePipe.transform(end,'yyyy-MM-dd');
    let reason = this.reason.value
    // let 
    console.log('leaveTypeId'+leaveTypeId);
    console.log('start' + start);
    console.log('end' + end);
    console.log('reason' + reason);
    
  }
  
}
