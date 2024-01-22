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
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService, IMapper mapper)
        {
            _leaveRequestService=leaveRequestService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<LeaveRequest>> PostLeaveRequest(LeaveRequestDTO leaveRequestDto)
        {
            LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDto);
            LeaveRequest result= _leaveRequestService.addLeaveRequest(leaveRequest);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Leave days not available");
        }
        /*private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequests.Any(e => e.RequestId == id);
        }*/
    }
}
