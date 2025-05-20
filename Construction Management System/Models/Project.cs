using System.ComponentModel.DataAnnotations;

namespace ConstructionManagementSystem.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal Budget { get; set; }

        [Required]
        public string Status { get; set; }

        // Manager (User)
        public int? ManagerId { get; set; }
        public User Manager { get; set; }

        // Tasks in the project
        public ICollection<ProjectTask> Tasks { get; set; }
    }
}
