﻿using Microsoft.EntityFrameworkCore;
using PetHome.SharedKernel.ValueObjects.Discussion.Relation;
using PetHome.Discussions.Domain;

namespace DiscussionService.Application.Database.Interfaces;
public interface IDiscussionRepository
{
    public Task AddDiscussion(Discussion discussion);

    public Task AddDiscussion(IEnumerable<Discussion> discussions);

    public Task RemoveDiscussion(Discussion discussion);

    public Task RemoveDiscussion(IEnumerable<Discussion> discussions);

    public Task UpdateDiscussion(Discussion discussion);

    public Task UpdateDiscussion(IEnumerable<Discussion> discussions);

    public Task<Discussion?> GetDiscussionById(Guid discussionId, CancellationToken ct);

    public Task<IReadOnlyList<Discussion>> GetDiscussionByRelationId(Guid relationId, CancellationToken ct);

    public Task<IReadOnlyList<Discussion>> GetDiscussionByStatus(DiscussionStatus status, CancellationToken ct);



    public Task AddRelation(Relation relation);

    public Task AddRelation(IEnumerable<Relation> relations);

    public Task RemoveRelation(Relation relation);

    public Task RemoveRelation(IEnumerable<Relation> relations);

    public Task UpdateRelation(Relation relation);

    public Task UpdateRelation(IEnumerable<Relation> relations);

    public Task<Relation?> GetRelationById(Guid relationId, CancellationToken ct);

    public Task<Relation?> GetRelationByName(string relationName, CancellationToken ct);
}
