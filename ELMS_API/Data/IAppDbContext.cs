using ELMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ELMS_API.Data
{
    public interface IAppDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Manager> Managers { get; set; }
        DbSet<LeaveBalance> LeaveBalances { get; set; }
        DbSet<LeaveType> LeaveTypes { get; set; }
        DbSet<TeamMember> TeamMembers { get; set; }
        DbSet<LeaveRequest> LeaveRequests { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
