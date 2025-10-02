using System;
using Microsoft.EntityFrameworkCore;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Countries> Countries => Set<Countries>();
    public DbSet<Regions> Regions => Set<Regions>();
    public DbSet<Cities> Cities => Set<Cities>();
    public DbSet<Companies> Companies => Set<Companies>();
    public DbSet<Branches> Branches => Set<Branches>();
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<Companies>()
            .OwnsOne(c => c.Ukniu, u =>
            {
                u.Property(x => x.Value).HasColumnName("Ukniu").IsRequired();
            });

        base.OnModelCreating(modelBuilder);
    }
}
