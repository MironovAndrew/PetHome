﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.Core.ValueObjects.User;
public record UserName
{
    public string Value { get; }
    private UserName(string value)
    {
        Value = value;
    }

    public static Result<UserName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.Validation("UserName");

        return new UserName(value);
    }

    public static implicit operator string(UserName name) => name.Value;
}
