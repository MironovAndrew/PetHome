﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using PetHome.Accounts.Application.Database.Repositories;
using PetHome.Accounts.Infrastructure.Database;
using PetHome.Accounts.Infrastructure.Database.Repositories;
using PetHome.Framework.Database;
using Respawn;
using System.Data.Common;
using Testcontainers.PostgreSql;
using Xunit;

namespace PetHome.Accounts.IntegrationTests.IntegrationFactories;

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
    private AuthorizationDbContext _dbContext;
    private IAuthenticationRepository _repository;
    private IUnitOfWork _unitOfWork;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("AccountTesting");
        builder.ConfigureTestServices(ConfigureDefault);
    }

    private void ConfigureDefault(IServiceCollection services)
    {
        services.RemoveAll(typeof(AuthorizationDbContext));
        services.RemoveAll(typeof(IAuthenticationRepository));
        //services.RemoveAll(typeof(ICreateUserContract));
        //services.RemoveAll(typeof(IGetRoleContract));
        //services.RemoveAll(typeof(IGenerateAccessTokenContract));
        //services.RemoveAll(typeof(IGenerateRefreshTokenContract));


        _repository = new AuthenticationRepository(new AuthorizationDbContext(_dbContainer.GetConnectionString()), default);
        _unitOfWork = new UnitOfWork(new AuthorizationDbContext(_dbContainer.GetConnectionString()));

        services.AddScoped(_ => new AuthorizationDbContext(_dbContainer.GetConnectionString()));
        services.AddScoped(_ => _repository);
        services.AddScoped(_ => _unitOfWork);

        //services.AddScoped<ICreateUserContract, CreateUserUseCase>();
        //services.AddScoped<IGetRoleContract, GetRoleIdByNameUseCase>();
        //services.AddScoped<IGenerateAccessTokenContract, GenerateAccessTokenUseCase>();
        //services.AddScoped<IGenerateRefreshTokenContract, GenerateRefreshTokenUseCase>();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        _dbContext = Services.CreateScope().ServiceProvider.GetRequiredService<AuthorizationDbContext>();

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
            SchemasToInclude = ["Account"]
        };
        _respawner = await Respawner.CreateAsync(
            _dbConnection,
            respawnerOptions);
    }
}
