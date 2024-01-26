export class LeaveRequest {
    requestId : number = 0
    EmployeeId : number = 0
    LeaveTypeId : number | undefined = 0
    StartDate : string | undefined  = ''
    EndDate : string | undefined  = ''
    Status : string = 'PENDING'
    Comments : string | null = ''
    dateSubmitted : string = ''
}
