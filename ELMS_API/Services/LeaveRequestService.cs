using ELMS_API.Data;
using ELMS_API.Interfaces;
using ELMS_API.Models;

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
      int noOfLeaveRequested = (int)(request.EndDate - request.StartDate).TotalDays;
      if(noOfLeaveAvailable >= noOfLeaveRequested) {
        _context.LeaveRequests.Add(request);
        _context.SaveChangesAsync();
        return request;
      }
      return null;
    }
    }
}
