﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.Accounts.Domain.Aggregates.User;
public record UserName
{
    private string Value { get;}
    public UserName(string value)
    {
        Value = value;
    }

    public static Result<UserName, Error> Create(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            return Errors.Validation("UserName");

        return new UserName(value);
    }
}
