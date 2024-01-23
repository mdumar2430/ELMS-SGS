using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELMS_API.Data;
using ELMS_API.Models;
using ELMS_API.DTO;
using AutoMapper;
using ELMS_API.Interfaces;

namespace ELMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IManagerService _managerService;

        public ManagersController(IMapper mapper, IManagerService managerService)
        {
            _mapper = mapper;
            _managerService = managerService;
        }

        [HttpPost]
        [Route("AddManager")]
        public async Task<ActionResult<Manager>> PostManager(ManagerDTO managerDto)
        {
            Manager manager = _mapper.Map<Manager>(managerDto);
            bool isAdded = _managerService.AddManager(manager);
            if (isAdded)
            {
                return Ok();
            }
            return BadRequest();
        }
        /*private bool ManagerExists(int id)
        {
            return _context.Managers.Any(e => e.ManagerId == id);
        }*/

        [HttpGet]
        [Route("GetPendingLeaveRequest")]
        public ActionResult GetPendingLeaveRequest(int managerId)
        {
            var pendingLeaveRequest = _managerService.GetPendingLeaveRequestsForManager(managerId);

            return Ok(pendingLeaveRequest);
        }
       
    }
}
