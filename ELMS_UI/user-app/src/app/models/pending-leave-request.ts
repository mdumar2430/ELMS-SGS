import { LeaveRequest } from "./leave-request.model"

export class PendingLeaveRequest {
    employeeName : string = ''
    leaveRequests : LeaveRequest = new LeaveRequest()
}
