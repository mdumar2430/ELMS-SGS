import { LeaveRequest } from "./leave-request.model"

export class PendingLeaveRequest {
    employeeName : string = ''
    leaveRequest : LeaveRequest = new LeaveRequest()
}
