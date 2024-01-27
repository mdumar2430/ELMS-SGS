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
      int noOfLeaveRequested ;

            if (request.EndDate == request.StartDate) { noOfLeaveRequested = 1; }
            else { noOfLeaveRequested = (int)(request.EndDate - request.StartDate).TotalDays; }
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
                int noOfDays;
                LeaveRequest approvalLeaveRequest = _context.LeaveRequests.FirstOrDefault(x => x.RequestId == leaveRequestID);
                if(approvalLeaveRequest != null)
                {
                    
                    if (approvalLeaveRequest.EndDate == approvalLeaveRequest.StartDate) { noOfDays = 1; }
                    else { noOfDays = (int)(approvalLeaveRequest.EndDate - approvalLeaveRequest.StartDate).TotalDays; }
                    if(noOfDays < 0) { throw new Exception("End date is beyond Start date"); }
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
            int noOfDays;

            LeaveRequest denyLeaveRequest = _context.LeaveRequests.FirstOrDefault(x => x.RequestId == leaveRequestID);
            if (denyLeaveRequest.EndDate == denyLeaveRequest.StartDate) { noOfDays = 1; }
            else { noOfDays = (int)(denyLeaveRequest.EndDate - denyLeaveRequest.StartDate).TotalDays; }
            if (denyLeaveRequest != null)
            {
                denyLeaveRequest.Status = "DENIED";
                denyLeaveRequest.DateResolved = DateTime.Now;
                leaveBalance.revertLeaveBalance(denyLeaveRequest.EmployeeId, denyLeaveRequest.LeaveTypeId, noOfDays);
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
