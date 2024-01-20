using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ELMS_API.Models
{
    public class LeaveRequest
    {
        [Key]
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime? DateResolved { get; set; }

        // Navigation properties for Employee (Many-to-One) and LeaveType (Many-to-One)
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
    }
}

