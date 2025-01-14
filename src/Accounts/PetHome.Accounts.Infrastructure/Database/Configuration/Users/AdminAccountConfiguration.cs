﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Accounts.Domain.Aggregates.User;
using PetHome.Accounts.Domain.Aggregates.User.Accounts;

namespace PetHome.Accounts.Infrastructure.Database.Configuration.Users;
public class AdminAccountConfiguration : IEntityTypeConfiguration<AdminAccount>
{
    public void Configure(EntityTypeBuilder<AdminAccount> builder)
    {
        builder.ToTable("admin_accounts");

        builder.HasKey(i => i.UserId);
        builder.Property(i => i.UserId)
            .HasConversion(
                i => i.Value,
                value => UserId.Create(value).Value)
            .HasColumnName("user_id");
    }
}
