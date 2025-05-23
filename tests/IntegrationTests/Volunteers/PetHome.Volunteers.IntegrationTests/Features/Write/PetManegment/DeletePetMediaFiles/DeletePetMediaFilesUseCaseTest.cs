﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Volunteers.Application.Dto.Pet;
using PetHome.Volunteers.Application.Features.Write.PetManegment.DeletePetMediaFiles;
using PetHome.Volunteers.IntegrationTests.IntegrationFactories;
using Xunit;
namespace PetHome.Volunteers.IntegrationTests.Features.Write.PetManegment.DeletePetMediaFiles;
public class DeletePetMediaFilesUseCaseTest : FileProviderFactory
{
    private readonly ICommandHandler<string, DeletePetMediaFilesCommand> _sut;

    public DeletePetMediaFilesUseCaseTest(IntegrationTestFactory factory) : base(factory)
    {
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<string, DeletePetMediaFilesCommand>>();
    }

    [Fact]
    public async void Delete_pet_media_files()
    {
        //array
        await SeedVolunteersWithAggregates();
        var pet = _writeDbContext.Volunteers.SelectMany(p => p.Pets).First();

        string bucketName = "photos";
        List<string> fileNames = new List<string>() { "99482373434.png", "3245345434.png", "845234123434.png", };
        DeletePetMediaFilesDto deletePetMediaFilesDto = new DeletePetMediaFilesDto(
            pet.Id,
            bucketName,
            fileNames);
        DeletePetMediaFilesCommand command = new DeletePetMediaFilesCommand(
            pet.VolunteerId,
            deletePetMediaFilesDto,
            _fileServiceMock);

        //act
        var result = await _sut.Execute(command, CancellationToken.None);

        //assert
        Assert.True(result.IsSuccess);
    }
}
