using Microsoft.EntityFrameworkCore;
using System.Reflection;
using violet.backend.Entities;

namespace violet.backend.Infrastructures;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<FemaleUser> FemaleUsers { get; set; }
    public DbSet<MaleUser> MaleUsers { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ReSharper disable once AssignNullToNotNullAttribute
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext)));

        base.OnModelCreating(modelBuilder);
    }
}
