using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence.Configurations;

public class RegionsConfiguration: IEntityTypeConfiguration<Regions>
{
    public void Configure(EntityTypeBuilder<Regions> builder)
    {
        builder.ToTable("Regions");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(r => r.Country)
            .WithMany(c => c.Regions)
            .HasForeignKey(r => r.CountryId);
        
        builder.HasMany(r => r.Cities)
            .WithOne(c => c.Region)
            .HasForeignKey(c => c.RegionId);
    }
}
