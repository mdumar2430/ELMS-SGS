using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELMS_API.Data;
using ELMS_API.Models;
using AutoMapper;
using ELMS_API.DTO;
using ELMS_API.Interfaces;

namespace ELMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypesController(IMapper mapper,ILeaveTypeService leaveTypeService)
        {
            _mapper = mapper;
            _leaveTypeService = leaveTypeService;
        }

        [HttpPost]
        public async Task<ActionResult<LeaveType>> PostLeaveType(LeaveTypeDTO leaveTypeDto)
        {
            LeaveType leaveType = _mapper.Map<LeaveType>(leaveTypeDto);
            //_.Add(leaveType);
            //await _context.SaveChangesAsync();

            return Ok(true);
        }
        private bool LeaveTypeExists(int id)
        {
            return _leaveTypeService.leaveTypeExist(id);
        }
    }
}
