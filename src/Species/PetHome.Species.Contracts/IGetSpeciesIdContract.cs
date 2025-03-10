﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;
using PetHome.Core.ValueObjects.PetManagment.Species;

namespace PetHome.Species.Contracts;
public interface IGetSpeciesIdContract
{
    public Task<Result<SpeciesId, Error>> Execute(string name, CancellationToken ct); 
}
