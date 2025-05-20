using System.ComponentModel.DataAnnotations;

namespace ConstructionManagementSystem.Models
{
    public class ProjectTask
    {
        [Key] // 👈 Add this line
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; }

        public string? Description { get; set; }

        // Foreign key to Project
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        // Foreign key to Assigned User
        public int? AssignedTo { get; set; }
        public User AssignedUser { get; set; }
    }
}