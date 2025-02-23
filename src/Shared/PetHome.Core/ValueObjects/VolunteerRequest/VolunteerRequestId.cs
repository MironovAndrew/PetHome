﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.Core.ValueObjects.VolunteerRequest;
public class VolunteerRequestId : ComparableValueObject
{
    public Guid Value { get; }
    public VolunteerRequestId(Guid value)
    {
        Value = value;
    }

    public static Result<VolunteerRequestId, Error> Create(Guid value)
    {
        return new VolunteerRequestId(value);
    }
    public static Result<VolunteerRequestId, Error> Create()
    {
        return new VolunteerRequestId(Guid.NewGuid());
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(VolunteerRequestId id) => id.Value;
}
