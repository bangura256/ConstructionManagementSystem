using System.ComponentModel.DataAnnotations;
namespace ConstructionManagementSystem.Models;

public class Expense
{
    public int ExpenseId { get; set; }

    public int? ProjectId { get; set; }

    [Required]
    public string Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime ExpenseDate { get; set; }

    public int? RecordedBy { get; set; }
}
