﻿using CSharpFunctionalExtensions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Core.Constants;
using PetHome.Core.Extentions.ErrorExtentions;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Core.Response.ErrorManagment;
using PetHome.Core.Response.Validation.Validator;
using PetHome.Core.ValueObjects.Discussion;
using PetHome.Core.ValueObjects.Discussion.Relation;
using PetHome.Core.ValueObjects.User;
using PetHome.Discussions.Application.Database.Interfaces;
using PetHome.Discussions.Contracts.Messaging;
using PetHome.Discussions.Domain;
using PetHome.Framework.Database;

namespace PetHome.Discussions.Application.Features.Write.CreateDiscussionUsingContract;
public class CreateDiscussionUseCase
    : ICommandHandler<DiscussionId, CreateDiscussionCommand>
{
    private readonly IDiscussionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publisher;

    public CreateDiscussionUseCase(
        IDiscussionRepository repository,
        IPublishEndpoint publisher,
        [FromKeyedServices(Constants.DISCUSSION_UNIT_OF_WORK_KEY)] IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<DiscussionId, ErrorList>> Execute(
        CreateDiscussionCommand command, CancellationToken ct)
    {
        if (command.UsersIds.Count() > 2)
            return Errors.Validation("Пользователей не должно быть больше 2").ToErrorList();

        RelationId relationId = RelationId.Create(command.RelationId).Value;
        List<UserId> usersIds = command.UsersIds
            .Select(u => UserId.Create(u).Value)
            .ToList();
        Discussion discussion = Discussion.Create(relationId, usersIds).Value;

        var transaction = await _unitOfWork.BeginTransaction(ct);
        await _repository.AddDiscussion(discussion);
        await _unitOfWork.SaveChanges(ct);
        CreatedDiscussionEvent createdDiscussionEvent = new CreatedDiscussionEvent(
            discussion.Id,
            discussion.RelationId,
            discussion.Relation?.Name,
            discussion.UserIds.Select(u => u.Value));
        await _publisher.Publish(createdDiscussionEvent, ct);

        transaction.Commit();

        return discussion.Id;
    }

    public async Task<Result<DiscussionId, ErrorList>> Execute(
        IEnumerable<UserId> userIds, CancellationToken ct)
    {
        if (userIds.Count() > 2)
            return Errors.Validation("Пользователей не должно быть больше 2").ToErrorList();

        var transaction = await _unitOfWork.BeginTransaction(ct);

        RelationName relationName = RelationName.Create("test discussion name").Value;
        Relation relation = Relation.Create(relationName);
        RelationId relationId = RelationId.Create(relation.Id).Value;
        await _repository.AddRelation(relation);

        Discussion discussion = Discussion.Create(relationId, userIds).Value;
        await _repository.AddDiscussion(discussion);

        await _unitOfWork.SaveChanges(ct);
        transaction.Commit();

        return discussion.Id;
    }
}
