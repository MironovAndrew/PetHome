﻿using CSharpFunctionalExtensions;
using PetHome.SharedKernel.Responses.ErrorManagement;

namespace PetManagementService.Domain.PetManagment.PetEntity;
public class PetId : ComparableValueObject
{
    public Guid Value { get; }

    private PetId() { }
    private PetId(Guid value)
    {
        Value = value;
    }

    public static Result<PetId, Error> Create() => new PetId(Guid.NewGuid());

    public static Result<PetId, Error> Create(Guid id) => new PetId(id);

    public static Result<PetId, Error> CreateEmpty() => new PetId(Guid.Empty);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(PetId petId) => petId.Value;
}