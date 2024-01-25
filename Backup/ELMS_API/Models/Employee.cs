using System.ComponentModel.DataAnnotations;

namespace ELMS_API.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // Navigation property for Manager (One-to-One)
        public Manager Manager { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<LeaveBalance> LeaveBalances { get; set; }   
    }
}
