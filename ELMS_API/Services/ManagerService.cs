using ELMS_API.Data;
using ELMS_API.Interfaces;
using ELMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ELMS_API.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IAppDbContext _appDbContext;
        public ManagerService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool AddManager(Manager manager)
        {
            _appDbContext.Managers.Add(manager);
            return true;
        }
        public List<LeaveRequest> GetPendingLeaveRequestsForManager(int managerId)
        {
            var pendingLeaveRequests = _appDbContext.LeaveRequests
    .                                  Join(
                                       _appDbContext.TeamMembers,
            lr => lr.EmployeeId,
        tm => tm.EmployeeId,
        (lr, tm) => new { LeaveRequest = lr, TeamMember = tm }
    )
    .Where(joinResult => joinResult.TeamMember.ManagerId == managerId && joinResult.LeaveRequest.Status == "Pending")
    .Select(joinResult => joinResult.LeaveRequest)
    .ToList();

            return pendingLeaveRequests;
        }
    }
}
