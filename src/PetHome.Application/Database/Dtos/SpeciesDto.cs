﻿using PetHome.Application.Database.Dtos;

public class SpeciesDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public ICollection<BreedDto> Breeds { get; init; }
}
