﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHome.Accounts.Infrastructure.Database;
using PetHome.Discussions.Domain;

namespace PetHome.Discussions.Infrastructure.Database;
public class DiscussionDbContext : DbContext
{
     DbSet<Discussion> Discussions => Set<Discussion>();
     DbSet<Relation> Relations => Set<Relation>();

    private readonly string _conntecitonString;

    public DiscussionDbContext(string conntecitonString
        = "Host=host.docker.internal;Port=5434;Database=pet_home;Username=postgres;Password=postgres") 
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
        builder.HasDefaultSchema("Discussions");

        builder.ApplyConfigurationsFromAssembly(typeof(DiscussionDbContext).Assembly,
            type => type.FullName?.ToLower().Contains("database.configuration") ?? false);

        builder.ApplyConfigurationsFromAssembly(typeof(AuthorizationDbContext).Assembly,
            type => type.FullName?.ToLower().Contains("database.configuration") ?? false);
    }
}
