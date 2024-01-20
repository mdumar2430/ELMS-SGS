namespace ELMS_API.Interfaces
{
    public interface ILeaveBalanceService
    {
        public int getLeaveBalance(int empId, int leaveTypeId);
    }
}
