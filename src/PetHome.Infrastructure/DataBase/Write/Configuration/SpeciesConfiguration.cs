﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.PetManagment.PetEntity;

namespace PetHome.Infrastructure.DataBase.Write.Configuration;
public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("specieses");

        //id
        builder.HasKey(x => x.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value).Value)
            .IsRequired()
            .HasColumnName("Id");

        //name 
        builder.Property(s => s.Name)
            .HasConversion(
                n => n.Value,
                value => SpeciesName.Create(value).Value)
            .IsRequired()
            .HasColumnName("name");

        //breeds
        builder.HasMany(s => s.Breeds)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey(x => x.SpeciesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
