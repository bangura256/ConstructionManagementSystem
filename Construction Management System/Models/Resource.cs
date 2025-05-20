using System.ComponentModel.DataAnnotations;

namespace ConstructionManagementSystem.DTOs;


public class Resource
{
    public int ResourceId { get; set; }

    [Required]
    public string ResourceName { get; set; }

    public int Quantity { get; set; }

    public string Unit { get; set; }

    public decimal Cost { get; set; }

    public int? ProjectId { get; set; }
}
