﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using PetHome.Accounts.Application.Database.Repositories;
using PetHome.Accounts.Application.Features.Contracts.AuthManagement.GetRole;
using PetHome.Discussions.Infrastructure.Database.Write;
using PetHome.Discussions.Infrastructure.Database.Write.Repositories;
using Respawn;
using System.Data.Common;
using Testcontainers.PostgreSql;
using Xunit;

namespace PetHome.Discussions.IntegrationTests.IntegrationFactories;

public class IntegrationTestFactory
    : WebApplicationFactory<PetHome.API.Program>, IAsyncLifetime
{
    protected readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres")
        .WithDatabase("pet_home_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private Respawner _respawner;
    private DbConnection _dbConnection; 
    private DiscussionDbContext _dbContext;
    private DiscussionRepository _repository;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("DiscussionTesting");
        builder.ConfigureTestServices(ConfigureDefault);
    }

    private void ConfigureDefault(IServiceCollection services)
    {
        services.RemoveAll(typeof(DiscussionDbContext));
        services.RemoveAll(typeof(IAuthenticationRepository));
        //services.RemoveAll(typeof(ICreateUserContract));
        services.RemoveAll(typeof(IGetRoleContract)); 
         
        _repository = new DiscussionRepository(new DiscussionDbContext(_dbContainer.GetConnectionString()));  
        services.AddScoped(_ => _repository); 
        services.AddScoped(_ => new DiscussionDbContext(_dbContainer.GetConnectionString()));

        //services.AddScoped<ICreateUserContract, CreateUserUseCase>();
        services.AddScoped<IGetRoleContract, GetRoleIdByNameUseCase>(); 
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        _dbContext = Services.CreateScope().ServiceProvider.GetRequiredService<DiscussionDbContext>();

        await _dbContext.Database.EnsureCreatedAsync();

        await InilizeRespawner();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
        await ResetDatabaseAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        if (_respawner is not null)
            await _respawner.ResetAsync(_dbConnection);
    }

    private async Task InilizeRespawner()
    {
        await _dbConnection.OpenAsync();

        RespawnerOptions respawnerOptions = new RespawnerOptions()
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["Discussions"]
        };
        _respawner = await Respawner.CreateAsync(
            _dbConnection,
            respawnerOptions);
    } 
}
