using AutoMapper;
using ELMS_API.DTO;
using ELMS_API.Models;

namespace ELMS_API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<LeaveBalance, LeaveBalanceDTO>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestDTO>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDTO>().ReverseMap();
            CreateMap<Manager, ManagerDTO>().ReverseMap();
            CreateMap<TeamMember, TeamMemberDTO>().ReverseMap();
        }
    }
}
