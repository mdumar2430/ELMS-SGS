import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import {MatSidenavModule} from '@angular/material/sidenav';
import { UserService } from '../../services/user.service';
import {MatListModule} from '@angular/material/list';
@Component({
  selector: 'sidenavbar',
  standalone: true,
  imports: [RouterOutlet, MatSidenavModule, MatListModule, RouterLink],
  templateUrl: './side-nav-bar.component.html',
  styleUrl: './side-nav-bar.component.css'
})
export class SideNavBarComponent {

  menuItems_user = [
  {
    name : "Request Leave",
    routeTo : "/leave-request"
  },
  {
    name: "Leave Status",
    routeTo : "/"
  }
]

  menuItems_manager = [
    {
      name : "Approve/Reject Leave Requests",
      routeTo : "/"
    },
    {
      name : "Request Leave",
      routeTo : "/leave-request"
    },
    {
      name: "Leave Status",
      routeTo : "/"
      
    }
  ]
  items = this.getMenuItems();

  constructor(public userService:UserService){

  }

  getMenuItems(){
    if(sessionStorage.getItem('role')=='Manager')
      return this.menuItems_manager;
    return this.menuItems_user;
  }
}
