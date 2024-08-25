using Microsoft.EntityFrameworkCore;
using mopo_flo_backend.Entities;

namespace mopo_flo_backend.Infrastructures;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TelegramUser> TelegramUsers { get; set; }
    public DbSet<PeriodLog> PeriodLogs { get; set; }
    public DbSet<Profile> Profiles { get; set; }
}