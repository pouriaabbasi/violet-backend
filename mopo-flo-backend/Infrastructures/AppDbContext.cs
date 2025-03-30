using Microsoft.EntityFrameworkCore;
using violet.backend.Entities;
using violet.backend.Entities.Configuration;

namespace violet.backend.Infrastructures;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
