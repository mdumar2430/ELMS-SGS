using AutoMapper;
using ELMS_API.Data;
using ELMS_API.DTO;
using ELMS_API.Interfaces;
using ELMS_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ELMS_API.Services
{
    public class LeaveBalanceService : ILeaveBalanceService
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public LeaveBalanceService(IAppDbContext _appDbContext,IMapper mapper)
        {
            this._appDbContext = _appDbContext;
            this._mapper = mapper;
        }
        public int getLeaveBalance(int empId, int leaveTypeId)
        {
            var rows = _appDbContext.LeaveBalances.Where(x => x.EmployeeId == empId && x.LeaveTypeId == leaveTypeId);
            var leaveBalance = rows.Select(x => x.Balance).Single();
            return leaveBalance;
        }
        public bool updateLeaveBalance(int empId, int leaveTypeId, int noOfDays)
        {
            var rows = _appDbContext.LeaveBalances.Where(x => x.EmployeeId == empId && x.LeaveTypeId == leaveTypeId).ToList();
            LeaveBalance row = rows.First();
            row.Balance = row.Balance - noOfDays;
            _appDbContext.SaveChanges();
            return true;
        }
        public bool revertLeaveBalance(int empId, int leaveTypeId, int noOfDays)
        {
            var rows = _appDbContext.LeaveBalances.Where(x => x.EmployeeId == empId && x.LeaveTypeId == leaveTypeId).ToList();
            LeaveBalance row = rows.First();
            row.Balance = row.Balance + noOfDays;
            _appDbContext.SaveChanges();
            return true;
        }
        public List<LeaveBalanceDTO> getLeaveBalanceById(int  empId)
        {
            Employee employee = _appDbContext.Employees.FirstOrDefault(x => x.EmployeeId == empId);
            if (employee == null) { throw new Exception("Employee does not exist"); }
            List<LeaveBalanceDTO> list = _mapper.Map<List<LeaveBalanceDTO>>(_appDbContext.LeaveBalances.Where(lb => lb.EmployeeId == empId).ToList());
            return list;
        }
    }
}
