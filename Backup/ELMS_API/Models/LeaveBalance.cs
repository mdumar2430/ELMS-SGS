using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ELMS_API.Models
{
    public class LeaveBalance
    {
        [Key]
        public int BalanceId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int Balance { get; set; }

        // Navigation properties for Employee (Many-to-One) and LeaveType (Many-to-One)
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
    }
}
