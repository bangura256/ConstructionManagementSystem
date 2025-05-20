using System.ComponentModel.DataAnnotations;

namespace ConstructionManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        // Projects this user manages
        public ICollection<Project> ManagedProjects { get; set; }

        // Tasks assigned to this user
        public ICollection<ProjectTask> AssignedTasks { get; set; }
    }
}
