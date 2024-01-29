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
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {

            _employeeService = employeeService;
            _mapper = mapper;
        }

        // POST: api/Employees
        [HttpPost]
        [Route("AddEmployee")]
        public ActionResult<Employee> PostEmployee(EmployeeDTO employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            Employee emp = _employeeService.AddEmployee(employee);
            if (emp != null)
            {
                return Ok(emp);
            }
            return BadRequest();

        }

        
    }
}
