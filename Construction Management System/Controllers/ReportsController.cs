using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construction_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports() =>
            await _context.Reports.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            return report == null ? NotFound() : report;
        }

        [HttpPost]
        [Authorize(Roles = "Project Manager, Engineer")]
        public async Task<ActionResult<Report>> CreateReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReport), new { id = report.ReportId }, report);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Project Manager")]
        public async Task<IActionResult> UpdateReport(int id, Report report)
        {
            if (id != report.ReportId) return BadRequest();
            _context.Entry(report).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Project Manager")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return NotFound();
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
