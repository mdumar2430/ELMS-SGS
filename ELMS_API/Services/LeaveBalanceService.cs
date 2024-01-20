using ELMS_API.Data;
using ELMS_API.Interfaces;

namespace ELMS_API.Services
{
    public class LeaveBalanceService : ILeaveBalanceService
    {
        private readonly IAppDbContext _appDbContext;
        public LeaveBalanceService(IAppDbContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public int getLeaveBalance(int empId, int leaveTypeId)
        {
            var rows = _appDbContext.LeaveBalances.Where(x => x.EmployeeId == empId && x.LeaveTypeId == leaveTypeId);
            var leaveBalance = rows.Select(x => x.Balance).Single();
            return leaveBalance;
        }
    }
}
