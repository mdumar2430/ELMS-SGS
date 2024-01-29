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
    public class LeaveBalancesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILeaveBalanceService _leaveBalanceService;

        public LeaveBalancesController(AppDbContext context, IMapper mapper,ILeaveBalanceService leaveBalanceService)
        {
            _context = context;
            _mapper = mapper;
            _leaveBalanceService = leaveBalanceService;
        }

        // GET: api/LeaveBalances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveBalance>>> GetLeaveBalances()
        {
            return await _context.LeaveBalances.ToListAsync();
        }

        // GET: api/LeaveBalances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveBalance>> GetLeaveBalance(int id)
        {
            var leaveBalance = await _context.LeaveBalances.FindAsync(id);

            if (leaveBalance == null)
            {
                return NotFound();
            }

            return leaveBalance;
        }

        // PUT: api/LeaveBalances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveBalance(int id, LeaveBalance leaveBalance)
        {
            if (id != leaveBalance.BalanceId)
            {
                return BadRequest();
            }

            _context.Entry(leaveBalance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveBalanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LeaveBalances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveBalance>> PostLeaveBalance(LeaveBalanceDTO leaveBalanceDto)
        {
            LeaveBalance leaveBalance = _mapper.Map<LeaveBalance>(leaveBalanceDto);
            _context.LeaveBalances.Add(leaveBalance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveBalance", new { id = leaveBalance.BalanceId }, leaveBalance);
        }

        // DELETE: api/LeaveBalances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveBalance(int id)
        {
            var leaveBalance = await _context.LeaveBalances.FindAsync(id);
            if (leaveBalance == null)
            {
                return NotFound();
            }

            _context.LeaveBalances.Remove(leaveBalance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveBalanceExists(int id)
        {
            return _context.LeaveBalances.Any(e => e.BalanceId == id);
        }
    
    [HttpPost]
    [Route("GetLeaveBalanceById")]
    public ActionResult GetLeaveBalanceById([FromBody] int employeeId)
    {
        try
        {
            var leaveBlanceList = _leaveBalanceService.getLeaveBalanceById(employeeId);
            return Ok(leaveBlanceList);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
}
