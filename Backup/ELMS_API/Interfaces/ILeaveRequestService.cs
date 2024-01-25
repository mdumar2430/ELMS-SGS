using ELMS_API.DTO;
using ELMS_API.Models;

namespace ELMS_API.Interfaces
{
    public interface ILeaveRequestService
    {
        public LeaveRequest addLeaveRequest(LeaveRequest request);
        public bool approveLeaveRequest(int leaveRequestId);
        public List<PendingLeaveRequestDTO> GetPendingLeaveRequestsForManager(int managerId);
    }
}
