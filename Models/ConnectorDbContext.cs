using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Api.Models;

public class ConnectorDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ConnectorDb");
    }
}
