﻿using Microsoft.Extensions.DependencyInjection;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Species.Application.Features.Write.CreateBreed;
using PetHome.Species.IntegrationTests.IntegrationFactories;
using Xunit;
namespace PetHome.Species.IntegrationTests.Features.Write.CreateBreed;
public class CreateBreedUseCaseTest : SpeciesFactory
{
    private readonly ICommandHandler<Guid, CreateBreedCommand> _sut;
    public CreateBreedUseCaseTest(IntegrationTestFactory factory) : base(factory)
    { 
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, CreateBreedCommand>>();
    }

    [Fact]
    public async void Create_breed()
    {
        //array
        await SeedSpecies(1);
        var species = _writeDbContext.Species.First();
        var breedNames = new List<string>() { "Алабай", "Овчарка", "Питбуль" };

        CreateBreedCommand command = new CreateBreedCommand(species.Id, breedNames);

        //act
        var result = await _sut.Execute(command, CancellationToken.None);

        //assert
        Assert.True(result.IsSuccess);
    }
}
