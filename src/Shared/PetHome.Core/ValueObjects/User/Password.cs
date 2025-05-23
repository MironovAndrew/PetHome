﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.Core.ValueObjects.User;
public record Password
{
    public string Value { get; }
    private Password(string value)
    {
        Value = value;
    }

    public static Result<Password, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.Validation("Password");

        return new Password(value);
    }

    public static implicit operator string(Password password) => password.Value;
}
