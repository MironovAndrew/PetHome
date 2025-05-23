﻿using AccountService.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.SharedKernel.ValueObjects.MainInfo;
using PetHome.SharedKernel.ValueObjects.PetManagment.Extra;
using PetHome.SharedKernel.ValueObjects.User;
using System.Text.Json;

namespace AccountService.Infrastructure.Database.Configuration.Users;
public class VolunteerAccountConfiguration : IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.ToTable("volunteer_accounts");

        builder.HasKey(i => i.UserId);
        builder.Property(r => r.UserId)
            .HasConversion(
                i => i.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("user_id");


        builder.Property(d => d.StartVolunteeringDate)
            .HasConversion(
                i => i.Value,
                value => Date.Create(value).Value)
            .IsRequired()
            .HasColumnName("start_volunteering_date");


        builder.Property(r => r.Requisites)
            .HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<Requisites>>(json, JsonSerializerOptions.Default))
            .HasColumnName("requisites");


        builder.Property(c => c.Certificates)
            .HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<Certificate>>(json, JsonSerializerOptions.Default))
            .HasColumnName("certificates");


        //builder.Property(p => p.Pets)
        //    .HasConversion(
        //        u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
        //        json => JsonSerializer.Deserialize<IReadOnlyList<Pet>>(json, JsonSerializerOptions.Default))
        //    .HasColumnName("pets");


        builder.Property(d => d.IsDeleted)
            .HasColumnName("is_deleted");

        builder.Property(d => d.DeletionDate)
            .HasColumnName("soft_deleted_date");
    }
}
