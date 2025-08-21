using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Priority>().HasData(
            new Priority()
            {
                Id = 1,
                PriorityType = "High",
            },
              new Priority()
              {
                  Id = 2,
                  PriorityType = "Moderate",
              }, new Priority()
              {
                  Id = 3,
                  PriorityType = "Low",
              }
                  );
    }
}