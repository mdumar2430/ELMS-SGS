using ELMS_API.Data;
using ELMS_API.DTO;
using ELMS_API.Interfaces;
using ELMS_API.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ELMS_API.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IAppDbContext _context;
        private readonly ILeaveBalanceService leaveBalance;
        private readonly ILeaveTypeService _leaveTypeService;
    public LeaveRequestService(IAppDbContext context,ILeaveBalanceService leaveBalance,ILeaveTypeService leaveTypeService)
    {
        _context= context;
        this.leaveBalance= leaveBalance;
            _leaveTypeService= leaveTypeService;
    }
    public LeaveRequest addLeaveRequest(LeaveRequest request)
    {
            bool isOverlap = _context.LeaveRequests
            .Any(lr =>
            lr.EmployeeId == request.EmployeeId &&
            lr.Status != "DENIED" && // Consider only approved leave requests
            ((lr.StartDate <= request.StartDate && request.StartDate <= lr.EndDate) ||
             (lr.StartDate <= request.EndDate && request.EndDate <= lr.EndDate) ||
             (request.StartDate <= lr.StartDate && lr.EndDate <= request.EndDate)));
            if(isOverlap) { throw new Exception("Leave has been already requested on these days already"); }
            int noOfLeaveAvailable = leaveBalance.getLeaveBalance(request.EmployeeId, request.LeaveTypeId);
            int noOfLeaveRequested=0 ;

            if (request.EndDate == request.StartDate && request.StartDate.DayOfWeek != DayOfWeek.Saturday && request.StartDate.DayOfWeek != DayOfWeek.Sunday) { noOfLeaveRequested = 1; }
            else { noOfLeaveRequested = CalculateWeekdays(request.StartDate, request.EndDate); }
            if (noOfLeaveRequested == 0) { throw new Exception("Its a weekend"); }
            if (noOfLeaveAvailable >= noOfLeaveRequested) {
                leaveBalance.updateLeaveBalance(request.EmployeeId, request.LeaveTypeId, noOfLeaveRequested);
                _context.SaveChangesAsync();
                request.Status = "PENDING";
                _context.LeaveRequests.Add(request);
        _context.SaveChangesAsync();
        return request;
      }
      return null;
    }
        public bool approveLeaveRequest(int leaveRequestID)
        {
                int noOfLeaveRequested;
                LeaveRequest approvalLeaveRequest = _context.LeaveRequests.FirstOrDefault(x => x.RequestId == leaveRequestID);
                if(approvalLeaveRequest != null)
                {

                if (approvalLeaveRequest.EndDate == approvalLeaveRequest.StartDate && approvalLeaveRequest.StartDate.DayOfWeek != DayOfWeek.Saturday && approvalLeaveRequest.StartDate.DayOfWeek != DayOfWeek.Sunday) { noOfLeaveRequested = 1; }
                else { noOfLeaveRequested = CalculateWeekdays(approvalLeaveRequest.StartDate, approvalLeaveRequest.EndDate); }
                if (noOfLeaveRequested < 0) { throw new Exception("End date is beyond Start date"); }
                    approvalLeaveRequest.Status = "APPROVED";
                    approvalLeaveRequest.DateResolved = DateTime.Now;
                    //leaveBalance.updateLeaveBalance(approvalLeaveRequest.EmployeeId, approvalLeaveRequest.LeaveTypeId, noOfDays);
                    _context.SaveChangesAsync();
                    return true;
                }
                return false;
           
        }
        public bool denyLeaveRequest(int leaveRequestID)
        {
            int noOfLeaveRequested;

            LeaveRequest denyLeaveRequest = _context.LeaveRequests.FirstOrDefault(x => x.RequestId == leaveRequestID);
            if (denyLeaveRequest.EndDate == denyLeaveRequest.StartDate && denyLeaveRequest.StartDate.DayOfWeek != DayOfWeek.Saturday && denyLeaveRequest.StartDate.DayOfWeek != DayOfWeek.Sunday) { noOfLeaveRequested = 1; }
            else { noOfLeaveRequested = CalculateWeekdays(denyLeaveRequest.StartDate, denyLeaveRequest.EndDate); }
            if (denyLeaveRequest != null)
            {
                denyLeaveRequest.Status = "DENIED";
                denyLeaveRequest.DateResolved = DateTime.Now;
                leaveBalance.revertLeaveBalance(denyLeaveRequest.EmployeeId, denyLeaveRequest.LeaveTypeId, noOfLeaveRequested);
                _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public List<PendingLeaveRequestDTO> GetPendingLeaveRequestsForManager(int managerId)
        {
            var pendingLeaveRequests = _context.LeaveRequests
                .Join(_context.TeamMembers,
                    lr => lr.EmployeeId,
                    tm => tm.EmployeeId,
                    (lr, tm) => new { LeaveRequest = lr, TeamMember = tm }
                )
                .Where(joinResult => joinResult.TeamMember.ManagerId == managerId && joinResult.LeaveRequest.Status == "PENDING")
                .Select(joinResult => new PendingLeaveRequestDTO
                {
                    
                    EmployeeName = $"{joinResult.LeaveRequest.Employee.FirstName} {joinResult.LeaveRequest.Employee.LastName}",
                    LeaveRequest = joinResult.LeaveRequest,
                    LeaveTypeName = _leaveTypeService.getLeaveNameById(joinResult.LeaveRequest.LeaveTypeId),
                })
                .ToList();

            return pendingLeaveRequests;
        }

        public static int CalculateWeekdays(DateTime startDate, DateTime endDate)
        {
            int weekdays = 0;

            // Iterate through each day between the start and end dates
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Check if the current day is not Saturday (DayOfWeek.Saturday) or Sunday (DayOfWeek.Sunday)
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekdays++;
                }
            }

            return weekdays;
        }

    }
}
