using AutoMapper;
using ELMS_API.Data;
using ELMS_API.DTO;
using ELMS_API.Interfaces;
using ELMS_API.Models;

namespace ELMS_API.Services
{
    public class LeaveTypeService:ILeaveTypeService
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public LeaveTypeService(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public bool AddLeaveType(LeaveType leaveType)
        {
            _appDbContext.LeaveTypes.Add(leaveType);
            return true;
        }
        public List<LeaveTypeDTO> GetLeaveTypes()
        {
            List<LeaveTypeDTO> leaveTypeList = new List<LeaveTypeDTO>();   
            var leaveTypes = _mapper.Map<List<LeaveTypeDTO>>(_appDbContext.LeaveTypes); 
            foreach (var leaveType in leaveTypes) 
            {
               leaveTypeList.Add(leaveType);
            }
            return leaveTypeList;
        }
        public string getLeaveNameById(int leaveTypeId)
        {
            var rows = _appDbContext.LeaveTypes.Where(x => x.LeaveTypeId == leaveTypeId);
            var leaveName = rows.Select(x => x.Name).Single();
            return leaveName;

        }
        public bool leaveTypeExist(int id)
        {
            return _appDbContext.LeaveTypes.Any(e => e.LeaveTypeId == id);
        }
    }
}
