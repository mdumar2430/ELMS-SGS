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
using ELMS_API.Services;
using Microsoft.AspNetCore.Authorization;

namespace ELMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveTypeService _leaveTypeService;


        public LeaveRequestsController(ILeaveRequestService leaveRequestService, IMapper mapper,ILeaveTypeService leaveTypeService)
        {
            _leaveRequestService=leaveRequestService;
            _mapper = mapper;
            _leaveTypeService=leaveTypeService;
        }

        [HttpPost]
        [Route("AddLeaveRequest")]
        [Authorize]
        public async Task<ActionResult<LeaveRequest>> PostLeaveRequest(LeaveRequestDTO leaveRequestDto)
        {
            try
            {
                LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDto);
                LeaveRequest result = _leaveRequestService.addLeaveRequest(leaveRequest);
                if (result != null)
                {
                    return Ok(result);
                }
                string leaveName = _leaveTypeService.getLeaveNameById(leaveRequestDto.LeaveTypeId);
                return StatusCode(500, "Not enough "+leaveName+"s available");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("ApproveLeaveRequest")]
        [Authorize(Roles = "Manager")]
        public ActionResult ApprovePendingLeaveRequest([FromBody]int leaveRequestId)
        {
            try
            {
                bool isApproved = _leaveRequestService.approveLeaveRequest(leaveRequestId);
                if (isApproved)
                {
                    return Ok(isApproved);
                }
                return StatusCode(404, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("DenyLeaveRequest")]
        [Authorize(Roles = "Manager")]
        public ActionResult DenyPendingLeaveRequest([FromBody] int leaveRequestId)
        {
            bool isApproved = _leaveRequestService.denyLeaveRequest(leaveRequestId);
            if (isApproved)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("GetPendingLeaveRequest")]
        [Authorize(Roles ="Manager")]
        public ActionResult GetPendingLeaveRequest([FromBody]int managerId)
        {
            var pendingLeaveRequest = _leaveRequestService.GetPendingLeaveRequestsForManager(managerId);

            return Ok(pendingLeaveRequest);
        }
        [HttpPost]
        [Route("GetLeaveRequestById")]
        public ActionResult GetLeaveRequestById([FromBody] int employeeId)
        {
            try
            {
                var leaveRequestList = _leaveRequestService.getLeaveRequestById(employeeId);
                return Ok(leaveRequestList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}