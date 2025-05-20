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
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpensesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses() =>
            await _context.Expenses.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            return expense == null ? NotFound() : expense;
        }

        [HttpPost]
        [Authorize(Roles = "Project Manager, Engineer")]
        public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExpense), new { id = expense.ExpenseId }, expense);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Project Manager")]
        public async Task<IActionResult> UpdateExpense(int id, Expense expense)
        {
            if (id != expense.ExpenseId) return BadRequest();
            _context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Project Manager")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null) return NotFound();
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
