﻿namespace PetHome.Species.Application.Database;
public interface ISpeciesReadDbContext
{ 
    IQueryable<SpeciesDto> Species { get; } 
}