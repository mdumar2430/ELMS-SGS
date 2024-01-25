using System.ComponentModel.DataAnnotations;

namespace ELMS_API.Models
{
    public class LeaveType
    {
        [Key]
        public int LeaveTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultQuota { get; set; }

        // Navigation property for LeaveRequests (One-to-Many)
        public List<LeaveRequest> LeaveRequests { get; set; }
        public List<LeaveBalance> LeaveBalances { get; set; }
    }
}
