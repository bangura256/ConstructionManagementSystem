using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Models;
using ConstructionManagementSystem.DTOs;

namespace ConstructionManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectTask> Tasks => Set<ProjectTask>();
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Report> Reports => Set<Report>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table Names
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<ProjectTask>().ToTable("Tasks");
            modelBuilder.Entity<Resource>().ToTable("Resources");
            modelBuilder.Entity<Expense>().ToTable("Expenses");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<Report>().ToTable("Reports");

            // Unique Constraints
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            // Relationships

            // Project - Manager (User)
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany(u => u.ManagedProjects)
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Project - Tasks
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Assigned Tasks
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
