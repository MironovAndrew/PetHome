﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHome.Accounts.Domain.Aggregates.RolePermission;
using PetHome.Accounts.Domain.Aggregates.User;

namespace PetHome.Accounts.Infrastructure;
public class AuthorizationDbContext : IdentityDbContext<User, RoleId, Guid>
{
    public DbSet<User> Users => Set<User>();
    public DbSet<RoleId> Roles => Set<RoleId>();

    private readonly string _conntecitonString;

    public AuthorizationDbContext(string conntecitonString)
    {
        _conntecitonString = conntecitonString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_conntecitonString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }

    private ILoggerFactory CreateLoggerFactory() =>
       LoggerFactory.Create(builder => { builder.AddConsole(); });


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("users");
        builder.Entity<RoleId>().ToTable("roles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claim");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_token");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_login");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_role");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claim");
    }
}
