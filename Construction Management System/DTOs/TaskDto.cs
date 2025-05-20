namespace ConstructionManagementSystem.DTOs
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
    }
}
