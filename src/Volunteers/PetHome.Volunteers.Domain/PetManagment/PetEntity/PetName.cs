﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.Volunteers.Domain.PetManagment.PetEntity;
public record PetName
{
    public string Value { get; }

    private PetName() { }
    private PetName(string value)
    {
        Value = value;
    }

    public static Result<PetName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.Validation("Кличка");

        return new PetName(value);
    }
}