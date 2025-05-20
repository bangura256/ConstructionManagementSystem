using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.DTOs;
using ConstructionManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construction_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task == null ? NotFound() : task;
        }

        [HttpPost]
        [Authorize(Roles = "Project Manager, Engineer")]
        public async Task<ActionResult<ProjectTask>> CreateTask(TaskDto dto)
        {
            var task = new ProjectTask
            {
                TaskName = dto.TaskName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                ProjectId = dto.ProjectId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Project Manager, Engineer")]
        public async Task<IActionResult> UpdateTask(int id, TaskDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.TaskName = dto.TaskName;
            task.StartDate = dto.StartDate;
            task.EndDate = dto.EndDate;
            task.Status = dto.Status;
            task.ProjectId = dto.ProjectId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Project Manager, Engineer")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
