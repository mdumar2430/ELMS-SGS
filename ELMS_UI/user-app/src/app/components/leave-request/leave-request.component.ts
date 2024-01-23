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
  providers: [NativeDateAdapter],
})
export class LeaveRequestComponent {
  leaveTypeControl = new FormControl<Animal | null>(null, Validators.required);
  reason = new FormControl(null, Validators.required);
  leaveTypes : Animal[] = this.getLeaveTypes();
  animals: Animal[] = [
    {name: 'Dog', sound: 'Woof!'},
    {name: 'Cat', sound: 'Meow!'},
    {name: 'Cow', sound: 'Moo!'},
    {name: 'Fox', sound: 'Wa-pa-pa-pa-pa-pa-pow!'},
  ];
  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  getLeaveTypes(){
    return[
      {name: 'Dog', sound: 'Woof!'},
      {name: 'Cat', sound: 'Meow!'},
      {name: 'Cow', sound: 'Moo!'},
      {name: 'Fox', sound: 'Wa-pa-pa-pa-pa-pa-pow!'},
    ];
  }
  
}
