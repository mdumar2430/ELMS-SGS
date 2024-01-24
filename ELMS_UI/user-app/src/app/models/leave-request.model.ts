export class LeaveRequest {
    RequestId : number = 0
    EmployeeId : number = 0
    LeaveTypeId : number | undefined = 0
    StartDate : string | undefined  = ''
    EndDate : string | undefined  = ''
    Status : string = 'PENDING'
    Comments : string | null = ''
    DateSubmitted : string = ''
}
