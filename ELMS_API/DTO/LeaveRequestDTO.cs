namespace ELMS_API.DTO
{
    public class LeaveRequestDTO
    {
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime? DateResolved { get; set; }
    }
}
