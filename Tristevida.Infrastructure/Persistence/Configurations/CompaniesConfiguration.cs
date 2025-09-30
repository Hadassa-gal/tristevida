using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence.Configurations;

public class CompaniesConfiguration : IEntityTypeConfiguration<Companies>
{
    public void Configure(EntityTypeBuilder<Companies> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);


        builder.Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(c => c.Ukniu, ukniu =>
        {
            ukniu.Property(u => u.Value)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("Ukniu");
        });

        builder.HasOne(c => c.City)
            .WithMany(c => c.Companies)
            .HasForeignKey(c => c.CityId);

        builder.HasMany(c => c.Branches)
            .WithOne(b => b.Company)
            .HasForeignKey(b => b.CompanyId);
    }
}
