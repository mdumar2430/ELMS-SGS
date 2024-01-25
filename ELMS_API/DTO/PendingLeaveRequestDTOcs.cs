using ELMS_API.Models;

namespace ELMS_API.DTO
{
    public class PendingLeaveRequestDTO
    {
        public string EmployeeName { get; set; }
        public LeaveRequest LeaveRequest { get; set; }

    }
}
