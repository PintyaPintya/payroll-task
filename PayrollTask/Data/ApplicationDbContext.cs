using Microsoft.EntityFrameworkCore;
using PayrollTask.Models.Domain;

namespace PayrollTask.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    public DbSet<Employee> Employees { get; set; }
    public DbSet<PTask> PTasks { get; set; }
    public DbSet<PTaskStatus> PTaskStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PTaskStatus>().HasData(
            new PTaskStatus { Id = 1, Name = "Assigned" },
            new PTaskStatus { Id = 2, Name = "Submitted" },
            new PTaskStatus { Id = 3, Name = "Under Review" },
            new PTaskStatus { Id = 4, Name = "Approved" },
            new PTaskStatus { Id = 5, Name = "Rejected" }
        );

        base.OnModelCreating(modelBuilder);
    }
}
