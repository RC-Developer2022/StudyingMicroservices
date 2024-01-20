using AccessApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessApplication.Context;

public class AppDbContext : DbContext
{
    public DbSet<UserAccess> UserAccess { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<string>().HaveMaxLength(150);
    }

}
