﻿using Microsoft.Extensions.DependencyInjection;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Core.ValueObjects.MainInfo;
using PetHome.Core.ValueObjects.RolePermission;
using PetHome.Core.ValueObjects.User;
using PetHome.VolunteerRequests.Application.Features.Write.CreateVolunteerRequest;
using PetHome.VolunteerRequests.IntegrationTests.IntegrationFactories;
using Xunit;

namespace PetHome.VolunteerRequests.IntegrationTests.Features.Write.CreateVolunteerRequest;
public class CreateVolunteerRequestUseCaseTest : VolunteerRequestFactory
{
    private readonly ICommandHandler<CreateVolunteerRequestCommand> _sut;

    public CreateVolunteerRequestUseCaseTest(IntegrationTestFactory factory) : base(factory)
    {
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<CreateVolunteerRequestCommand>>();
    }


    [Fact]
    public async void Create_volunteer_request()
    {
        //array 
        Email email = Email.Create("Emas2fgoiL123@mail.com").Value;
        UserName userName = UserName.Create("Ivanov Ivan").Value;
        RoleId roleId = await _getRoleContract.Execute("admin", CancellationToken.None);
        UserId userId = await _createUserContract.Execute(email, userName, roleId, CancellationToken.None);

        string volunteerInfo = "It's length enough volunteer requiest info";
        CreateVolunteerRequestCommand command = new CreateVolunteerRequestCommand(userId, volunteerInfo);

        //act
        var result = await _sut.Execute(command, CancellationToken.None);

        //assert
        Assert.True(result.IsSuccess);
    }
}
