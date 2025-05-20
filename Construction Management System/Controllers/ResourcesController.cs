using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConstructionManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Construction_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResourcesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResourcesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResources() =>
            await _context.Resources.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            return resource == null ? NotFound() : resource;
        }

        [HttpPost]
        [Authorize(Roles = "Project Manager")]
        public async Task<ActionResult<Resource>> CreateResource(ResourceDto dto)
        {
            var resource = new Resource
            {
                ResourceName = dto.ResourceName,
                Quantity = dto.Quantity,
                Unit = dto.Unit,
                Cost = dto.Cost,
                ProjectId = dto.ProjectId
            };

            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResource), new { id = resource.ResourceId }, resource);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Project Manager")]
        public async Task<IActionResult> UpdateResource(int id, ResourceDto dto)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null) return NotFound();

            resource.ResourceName = dto.ResourceName;
            resource.Quantity = dto.Quantity;
            resource.Unit = dto.Unit;
            resource.Cost = dto.Cost;
            resource.ProjectId = dto.ProjectId;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Project Manager")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null) return NotFound();
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
