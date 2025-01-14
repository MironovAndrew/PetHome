﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.Species.Domain.SpeciesManagment.BreedEntity;
public record BreedId
{
    public Guid Value { get; }

    private BreedId() { }
    private BreedId(Guid value)
    {
        Value = value;
    }

    public static Result<BreedId, Error> Create() => new BreedId(Guid.NewGuid());

    public static Result<BreedId, Error> Create(Guid id) => new BreedId(id);

    public static Result<BreedId, Error> CreateEmpty() => new BreedId(Guid.Empty);

    public static implicit operator Guid(BreedId breedId)
    {
        if (breedId == null)
            throw new ArgumentNullException();

        return breedId.Value;
    }
}