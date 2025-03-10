using Microsoft.EntityFrameworkCore;
using PayrollTask.Models.Domain;

namespace PayrollTask.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    public DbSet<Employee> Employees { get; set; }
    public DbSet<PTask> PTasks { get; set; }
    public DbSet<PTaskStatus> PTaskStatuses { get; set; }
}
