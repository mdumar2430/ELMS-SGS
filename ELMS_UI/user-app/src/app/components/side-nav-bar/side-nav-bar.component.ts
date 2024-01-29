import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import {MatSidenavModule} from '@angular/material/sidenav';
import { UserService } from '../../services/user.service';
import {MatListModule} from '@angular/material/list';
import {MatBadgeModule} from '@angular/material/badge';
import { LeaveService } from '../../services/leave.service';
@Component({
  selector: 'sidenavbar',
  standalone: true,
  imports: [RouterOutlet, MatSidenavModule, MatListModule, RouterLink, MatBadgeModule],
  templateUrl: './side-nav-bar.component.html',
  styleUrl: './side-nav-bar.component.css'
})
export class SideNavBarComponent {
  manager_id = Number(sessionStorage.getItem('managerId'))
  pendingList_count :any= 'abc'
  menuItems_user = [
  {
    name : "Request Leave",
    routeTo : "/leave-request"
  },
  {
    name : "My Leave Status",
    routeTo : "/leaveStatus"
  },
  {
    name : "Leave Info",
    routeTo : "/leaveInfo"
  }
]

  menuItems_manager = [
    {
      name : "Pending Leave Requests",
      routeTo : "/pendingLeaveRequests"
    },
    {
      name : "Request Leave",
      routeTo : "/leave-request"
    },
    {
      name : "My Leave Status",
      routeTo : "/leaveStatus"
    },
    {
      name : "Leave Info",
      routeTo : "/leaveInfo"
    }
  ]

  items = this.getMenuItems();

  constructor(public userService:UserService, public leaveService:LeaveService){

  }

  ngOnInit(){
    if(this.manager_id>0){
      this.leaveService.getPendingLeaveRequest(this.manager_id)
      .subscribe({
        next: (res) => {
          this.leaveService.noOfPendingLeaveRequests = res.length
        }
      })
    }
  }

  getMenuItems(){
    if(sessionStorage.getItem('role')=='Manager')
      return this.menuItems_manager;
    return this.menuItems_user;
  }
}
