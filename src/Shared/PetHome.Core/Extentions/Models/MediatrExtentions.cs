﻿using MediatR;
using PetHome.Core.Models;

namespace PetHome.Core.Extentions.Models;
public static class MediatrExtentions
{
    public static void Publish<TId>(
        this IPublisher publisher, DomainEntity<TId> domainEntity, CancellationToken ct)
        where TId : IComparable<TId>
    {
        domainEntity.DomainEvents.Select(async e => await publisher.Publish(e, ct));
        domainEntity.ClearDomainEvents();
    }
}
