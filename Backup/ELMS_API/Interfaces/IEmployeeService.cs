using ELMS_API.Models;

namespace ELMS_API.Interfaces
{
    public interface IEmployeeService
    {
        public Employee AddEmployee(Employee employee);
        public string GetEmployeeNameByEmployeeId(int empID);
    }
}
