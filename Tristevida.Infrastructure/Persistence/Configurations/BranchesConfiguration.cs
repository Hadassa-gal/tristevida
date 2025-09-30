using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence.Configurations;

public class BranchesConfiguration : IEntityTypeConfiguration<Branches>
{
    public void Configure(EntityTypeBuilder<Branches> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Number_Comercial)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Contact_Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(b => b.City)
            .WithMany(c => c.Branches)  
            .HasForeignKey(b => b.CityId);

        builder.HasOne(b => b.Company)
            .WithMany(c => c.Branches)
            .HasForeignKey(b => b.CompanyId);

    }
}
