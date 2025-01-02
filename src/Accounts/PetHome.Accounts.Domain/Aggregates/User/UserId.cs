﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.Accounts.Domain.Aggregates.User;
public class UserId
{
    private Guid Value { get; set; }
    public UserId(Guid value)
    {
        Value = value;
    }

    public static Result<UserId, Error> Create(Guid value)
    {
        return new UserId(value);
    }
}
