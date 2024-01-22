using ELMS_API.Models;

namespace ELMS_API.Interfaces
{
    public interface ILeaveTypeService
    {
        public bool AddLeaveType(LeaveType leaveType);
        public bool leaveTypeExist(int id);
    }
}
