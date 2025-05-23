﻿using AccountService.Application.Database.Repositories;
using AccountService.Contracts.Messaging.UserManagement;
using AccountService.Domain.Accounts;
using AccountService.Domain.Aggregates;
using CSharpFunctionalExtensions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Core.Application.Interfaces.FeatureManagement;
using PetHome.Core.Infrastructure.Database;
using PetHome.Core.Web.Extentions.ErrorExtentions;
using PetHome.SharedKernel.Constants;
using PetHome.SharedKernel.Responses.ErrorManagement;
using PetHome.SharedKernel.ValueObjects.MainInfo;
using PetHome.SharedKernel.ValueObjects.User;

namespace AccountService.Application.Features.Write.CreateParticipant;
public class CreateParticipantUseCase
    : ICommandHandler<UserId, CreateParticipantCommand>
{
    private readonly IAuthenticationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publisher;

    public CreateParticipantUseCase(
        IAuthenticationRepository repository,
        IPublishEndpoint publisher,
        [FromKeyedServices(Constants.Database.ACCOUNT_UNIT_OF_WORK_KEY)] IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<UserId, ErrorList>> Execute(CreateParticipantCommand command, CancellationToken ct)
    {
        var geRoleResult = await _repository.GetRole(ParticipantAccount.ROLE);
        if (geRoleResult.IsFailure)
            return geRoleResult.Error.ToErrorList();

        Role role = geRoleResult.Value;
        Email email = Email.Create(command.Email).Value;
        UserName userName = UserName.Create(command.UserName).Value;
        User user = User.Create(email, userName, role).Value;
        ParticipantAccount participant = ParticipantAccount.Create(user).Value;

        var transaction = await _unitOfWork.BeginTransaction(ct);
        await _repository.AddUser(user, ct);
        await _repository.AddParticipant(participant, ct);
        await _unitOfWork.SaveChanges(ct);

        CreatedParticipantEvent createdParticipantEvent = new CreatedParticipantEvent(
            user.Id, user.Email, user.UserName);
        await _publisher.Publish(createdParticipantEvent, ct);
        transaction.Commit();

        UserId userId = UserId.Create(user.Id).Value;
        return userId;
    }
}
