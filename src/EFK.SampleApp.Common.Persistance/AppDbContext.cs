namespace EFK.SampleApp.Common.Persistance;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Measurement> Measurements { get; set; }
}
