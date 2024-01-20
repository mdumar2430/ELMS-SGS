using ELMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ELMS_API.Data
{
    public class AppDbContext: DbContext , IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
