using ELMS_API.Data;
using ELMS_API.Interfaces;
using ELMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ELMS_API.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IAppDbContext _appDbContext;
        public ManagerService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool AddManager(Manager manager)
        {
            _appDbContext.Managers.Add(manager);
            return true;
        }
        public int GetManagerIdByEmployeeId(int employeeId)
        {
            var row = _appDbContext.Managers.FirstOrDefault(x=>x.EmployeeId == employeeId);
            if (row != null) {
                return row.ManagerId;
            }
            return 0;
        }
    }
}
