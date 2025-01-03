﻿using Microsoft.Extensions.DependencyInjection;
using PetHome.Application.Features.Write.PetManegment.ChangePetStatus;
using PetHome.Application.Interfaces.FeatureManagment;
using PetHome.Domain.PetManagment.PetEntity;
using PetHome.IntegrationTests.IntegrationFactories;
using Xunit;

namespace PetHome.IntegrationTests.Features.Write.PetManegment.ChangePetStatus;
public class ChangePetStatusUseCaseTest : BaseFactory
{
    private readonly ICommandHandler<string, ChangePetStatusCommand> _sut;
 
    public ChangePetStatusUseCaseTest(IntegrationTestFactory factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        _sut = scope.ServiceProvider.GetRequiredService<ICommandHandler<string, ChangePetStatusCommand>>();
    }

    [Fact]
    public async void Success_change_pet_status()
    {
        //array
        await SeedVolunteersWithAggregates();
        var pet = _writeDbContext.Volunteers.SelectMany(p => p.Pets).First();

        ChangePetStatusCommand command = new ChangePetStatusCommand(
            pet.VolunteerId, 
            pet.Id, 
            PetStatusEnum.isFree);

        //act
        var result = await _sut.Execute(command,CancellationToken.None);

        //assert
        Assert.True(result.IsSuccess);
    }
}
