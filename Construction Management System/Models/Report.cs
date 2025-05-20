using System.ComponentModel.DataAnnotations;

namespace ConstructionManagementSystem.Models;


public class Report
{
    public int ReportId { get; set; }

    public int? ProjectId { get; set; }

    [Required]
    public string ReportType { get; set; }

    public int? GeneratedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
