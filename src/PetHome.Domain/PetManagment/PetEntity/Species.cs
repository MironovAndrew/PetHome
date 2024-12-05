﻿namespace PetHome.Domain.PetManagment.PetEntity;
public class Species
{
    public Species() { }

    public SpeciesId Id { get; private set; }
    public SpeciesName Name { get; private set; }
    public IReadOnlyList<Breed> Breeds { get; private set; }
}