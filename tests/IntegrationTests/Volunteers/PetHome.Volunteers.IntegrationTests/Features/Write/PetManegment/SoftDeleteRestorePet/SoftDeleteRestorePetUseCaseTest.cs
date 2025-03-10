﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Volunteers.Application.Features.Write.PetManegment.SoftDeleteRestore;
using PetHome.Volunteers.IntegrationTests.IntegrationFactories;
using Xunit;
namespace PetHome.Volunteers.IntegrationTests.Features.Write.PetManegment.SoftDeleteRestorePet;
public class SoftDeleteRestorePetUseCaseTest : VolunteerFactory
{
    private readonly ICommandHandler<SoftDeleteRestorePetCommand> _sut;
    public SoftDeleteRestorePetUseCaseTest(IntegrationTestFactory factory) : base(factory)
    {
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<SoftDeleteRestorePetCommand>>();
    }

    [Fact]
    public async void Soft_delete_restore_pet()
    {
        //array
        await SeedVolunteersWithAggregates();
        var pet = _writeDbContext.Volunteers.SelectMany(p => p.Pets).First();
        SoftDeleteRestorePetCommand command = new SoftDeleteRestorePetCommand(
            pet.VolunteerId,
            pet.Id,
            true);

        //act
        var result = await _sut.Execute(command, CancellationToken.None);

        //assert
        Assert.True(result.IsSuccess);
    }
}
