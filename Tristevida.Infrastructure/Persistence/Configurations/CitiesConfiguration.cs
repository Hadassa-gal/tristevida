using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence.Configurations;

public class CitiesConfiguration: IEntityTypeConfiguration<Cities>
{
    public void Configure(EntityTypeBuilder<Cities> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(c => c.Region)
            .WithMany(r => r.Cities)
            .HasForeignKey(c => c.RegionId);

        builder.HasMany(c => c.Companies)
            .WithOne(co => co.City)
            .HasForeignKey(co => co.CityId);
        
        builder.HasMany(c => c.Branches)
            .WithOne(b => b.City)  
            .HasForeignKey(b => b.CityId);
    }
}
