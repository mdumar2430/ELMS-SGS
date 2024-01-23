﻿using ELMS_API.Data;
using ELMS_API.Interfaces;
using ELMS_API.Models;

namespace ELMS_API.Services
{
    public class LeaveTypeService:ILeaveTypeService
    {
        private readonly IAppDbContext _appDbContext;
        public LeaveTypeService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public bool AddLeaveType(LeaveType leaveType)
        {
            _appDbContext.LeaveTypes.Add(leaveType);
            return true;
        }
        public List<string> GetLeaveTypes()
        {
            List<string> leaveTypeList = new List<string>();   
            var leaveTypes = _appDbContext.LeaveTypes; 
            foreach (var leaveType in leaveTypes) 
            {
               leaveTypeList.Add(leaveType.Name);
            }
            return leaveTypeList;
        }
        public bool leaveTypeExist(int id)
        {
            return _appDbContext.LeaveTypes.Any(e => e.LeaveTypeId == id);
        }
    }
}
