using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence.Configurations;

public class CountriesConfigurations : IEntityTypeConfiguration<Countries>
{
    public void Configure(EntityTypeBuilder<Countries> builder)
    {
        builder.ToTable("Countries");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(c => c.Regions)
       .WithOne(r => r.Country)
       .HasForeignKey(r => r.CountryId);

    }
}
