import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { LeaveRequestComponent } from './components/leave-request/leave-request.component';
import { PendingRequestsComponent } from './components/pending-requests/pending-requests.component';
import { LeaveStatusComponent } from './components/leave-status/leave-status.component';
import { LeaveInfoComponent } from './components/leave-info/leave-info.component';

export const routes: Routes = [
    {
        path:'',
        component:LoginComponent
    },
    {
        path:'leave-request',
        component:LeaveRequestComponent,
    },
    {
        path:'pendingLeaveRequests',
        component:PendingRequestsComponent
    },
    {
        path:'leaveStatus',
        component: LeaveStatusComponent
    },
    {
        path:'leaveInfo',
        component: LeaveInfoComponent
    }
];
