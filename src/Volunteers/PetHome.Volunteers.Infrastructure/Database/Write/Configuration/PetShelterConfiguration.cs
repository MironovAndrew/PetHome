﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Volunteers.Domain.PetManagment.PetEntity;

namespace PetHome.Volunteers.Infrastructure.Database.Write.Configuration;
public class PetShelterConfiguration : IEntityTypeConfiguration<PetShelter>
{
    public void Configure(EntityTypeBuilder<PetShelter> builder)
    {
        builder.ToTable("shelters");

        //id
        builder.HasKey(x => x.Id);
        builder.Property(i => i.Id)
            .HasConversion(
                id => id.Value,
                value => PetShelterId.Create(value).Value)
            .IsRequired()
            .HasColumnName("id");

        //name
        builder.ComplexProperty(n => n.Name, br =>
        {
            br.Property(v => v.Value)
            .IsRequired()
            .HasColumnName("name");
        });
    }
}
