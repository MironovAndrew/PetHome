﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.Core.ValueObjects.Discussion;
public record DiscussionId
{
    public Guid Value { get; }
    public DiscussionId(Guid value)
    {
        Value = value;
    }

    public static Result<DiscussionId, Error> Create(Guid value) => new DiscussionId(value);

    public static Result<DiscussionId, Error> Create() => new DiscussionId(Guid.NewGuid());

    public static implicit operator Guid(DiscussionId id) => id.Value;
}
