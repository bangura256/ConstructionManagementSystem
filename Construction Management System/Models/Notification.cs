using System.ComponentModel.DataAnnotations;

namespace ConstructionManagementSystem.Models;


public class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    [Required]
    public string Message { get; set; }

    public bool IsRead { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
