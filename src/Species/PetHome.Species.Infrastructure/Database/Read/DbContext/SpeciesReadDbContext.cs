﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHome.Core.Constants;
using PetHome.Species.Application.Database;
using PetHome.Species.Application.Database.Dto;

namespace PetHome.Species.Infrastructure.Database.Read.DBContext;

public class SpeciesReadDbContext : DbContext, ISpeciesReadDbContext
{
    private readonly string _connectionString = Constants.DATABASE;
    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();

    public SpeciesReadDbContext(string connectionString
        = "Host=host.docker.internal;Port=5434;Database=pet_home;Username=postgres;Password=postgres")
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        optionBuilder.UseNpgsql(_connectionString);
        optionBuilder.UseSnakeCaseNamingConvention();
        optionBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionBuilder.EnableSensitiveDataLogging();
        optionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpeciesReadDbContext).Assembly,
            type => type.FullName?.ToLower().Contains("read.configuration") ?? false);
    }
}