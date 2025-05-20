// File: DTOs/ResourceDto.cs
namespace ConstructionManagementSystem.DTOs
{
    public class ResourceDto
    {
        public string ResourceName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public decimal Cost { get; set; }
        public int ProjectId { get; set; }
    }
}
