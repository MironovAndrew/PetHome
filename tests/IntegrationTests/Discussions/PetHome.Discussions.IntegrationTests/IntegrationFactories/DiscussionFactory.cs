﻿using Microsoft.Extensions.DependencyInjection;
using PetHome.Discussions.Domain;
using PetHome.Discussions.Infrastructure.Database.Write;
using PetHome.Discussions.IntegrationTests.Seeds;
using Xunit;

namespace PetHome.Discussions.IntegrationTests.IntegrationFactories;
public class DiscussionFactory
    : IClassFixture<IntegrationTestFactory>, IAsyncLifetime
{
    private readonly IntegrationTestFactory _factory;
    protected readonly IServiceScope _scope;
    private readonly SeedManager _seedManager; 
    protected readonly DiscussionDbContext _dbContext;

    //TODO
    //protected readonly ICreateUserContract _createUserContract;
    //protected readonly IGetRoleContract _getRoleContract;
    //protected readonly IGenerateAccessTokenContract _generateAccessTokenContract;
    //protected readonly IGenerateRefreshTokenContract _generateRefreshTokenContract;
    public DiscussionFactory(IntegrationTestFactory factory)
    {
        _factory = factory;
        _scope = factory.Services.CreateScope(); 
        _dbContext = _scope.ServiceProvider.GetRequiredService<DiscussionDbContext>();
        _seedManager = new SeedManager(factory);

        //_getRoleContract = _scope.ServiceProvider.GetRequiredService<IGetRoleContract>();
        ////_createUserContract = _scope.ServiceProvider.GetRequiredService<ICreateUserContract>();
        //_generateAccessTokenContract = _scope.ServiceProvider.GetRequiredService<IGenerateAccessTokenContract>();
        //_generateRefreshTokenContract = _scope.ServiceProvider.GetRequiredService<IGenerateRefreshTokenContract>();
    }


     protected async Task<IReadOnlyList<Discussion>> SeedDiscussions(int discussionsCountToSeed = 3)
         => await _seedManager.SeedDiscussions(5);
      
      
    public async Task DisposeAsync()
    {
        _scope.Dispose();
        await _factory.ResetDatabaseAsync();
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
}
