﻿using Microsoft.Extensions.DependencyInjection;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Core.ValueObjects.MainInfo;
using PetHome.Core.ValueObjects.User;
using PetHome.Volunteers.Application.Features.Write.VolunteerManegment.CreateVolunteer;
using PetHome.Volunteers.Contracts.Messaging;
using PetHome.Volunteers.IntegrationTests.IntegrationFactories;
using Xunit;
namespace PetHome.Volunteers.IntegrationTests.Features.Write.VolunteerManegment.CreateVolunteer;

public class CreateVolunteerUseCaseTest : VolunteerFactory
{
    private readonly ICommandHandler<Guid, CreateVolunteerCommand> _sut;
    public CreateVolunteerUseCaseTest(IntegrationTestFactory factory) : base(factory)
    {
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, CreateVolunteerCommand>>();
    }

    [Fact]
    public async void Create_volunteer()
    {
        //array
        FullNameDto fullNameDto = new FullNameDto("Иван", "Иванов");
        List<string> phoneNumbers = new List<string>() { "89383838733", "89332232332", "89777772332" };
        List<SocialNetworkDto> socialNetworks = new List<string>() { "vk.com/2943832", "tg/291221" }
        .Select(s => new SocialNetworkDto(s)).ToList();

        List<RequisitesesDto> requisiteses = new List<RequisitesesDto>();
        List<CertificateDto> certificates= new List<CertificateDto>();
        CreateVolunteerCommand command = new CreateVolunteerCommand(
            fullNameDto,
            "mail@mail.ru",
            "Описание",
            "username",
            DateTime.UtcNow,
            phoneNumbers,
            socialNetworks,
            certificates,
            requisiteses);

        //act
        var result = await _sut.Execute(command, CancellationToken.None);

        //assert
        Assert.True(result.IsSuccess);
    }
}
