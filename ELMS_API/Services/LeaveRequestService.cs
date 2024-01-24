using ELMS_API.Data;
using ELMS_API.DTO;
using ELMS_API.Interfaces;
using ELMS_API.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace ELMS_API.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IAppDbContext _context;
        private readonly ILeaveBalanceService leaveBalance;
    public LeaveRequestService(IAppDbContext context,ILeaveBalanceService leaveBalance)
    {
        _context= context;
        this.leaveBalance= leaveBalance;
    }
    public LeaveRequest addLeaveRequest(LeaveRequest request)
    {
      int noOfLeaveAvailable = leaveBalance.getLeaveBalance(request.EmployeeId, request.LeaveTypeId);
      int noOfLeaveRequested = (int)(request.EndDate - request.StartDate).TotalDays+1;
      if(noOfLeaveAvailable >= noOfLeaveRequested) {
                request.Status = "PENDING";
                _context.LeaveRequests.Add(request);
        _context.SaveChangesAsync();
        return request;
      }
      return null;
    }
        public bool approveLeaveRequest(int leaveRequestID)
        {
            
                LeaveRequest approvalLeaveRequest = _context.LeaveRequests.FirstOrDefault(x => x.RequestId == leaveRequestID);
                if(approvalLeaveRequest != null)
                {
                    approvalLeaveRequest.Status = "APPROVED";
                    approvalLeaveRequest.DateResolved = DateTime.Now;
                    int noOfLeaveRequested = (int)(approvalLeaveRequest.EndDate - approvalLeaveRequest.StartDate).TotalDays;
                    leaveBalance.updateLeaveBalance(approvalLeaveRequest.EmployeeId, approvalLeaveRequest.LeaveTypeId, noOfLeaveRequested);
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
                })
                .ToList();

            return pendingLeaveRequests;
        }
    }
}
