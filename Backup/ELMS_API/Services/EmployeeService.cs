using ELMS_API.Data;
using ELMS_API.Interfaces;
using ELMS_API.Models;
using System.ComponentModel.DataAnnotations;

namespace ELMS_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAppDbContext _appDbContext;
        public EmployeeService(IPasswordHasher passwordHasher, IAppDbContext appDbContext)
        {
            _passwordHasher = passwordHasher;
            _appDbContext = appDbContext;
        }

        public Employee AddEmployee(Employee employee)
        {
            if (employee!=null)
            {
                employee.Password = _passwordHasher.Hash(employee.Password);
                _appDbContext.Employees.Add(employee);
                _appDbContext.SaveChanges();
                return employee;
            }
            return null;
        }
        public string GetEmployeeNameByEmployeeId(int empID)
        {
            var row = _appDbContext.Employees.FirstOrDefault(x => x.EmployeeId == empID);
            if (row != null)
            {
                return row.FirstName + " " + row.LastName;
            }
            return null;
        }
    }
}
