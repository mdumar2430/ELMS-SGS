using ELMS_API.Models;

namespace ELMS_API.Interfaces
{
    public interface IManagerService
    {
        public bool AddManager(Manager manager);
        public int GetManagerIdByEmployeeId(int employeeId);
    }
}
