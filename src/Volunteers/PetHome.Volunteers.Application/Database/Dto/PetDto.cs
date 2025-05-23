﻿using FilesService.Core.Dto.File;
using PetHome.Core.ValueObjects.PetManagment.Pet;

namespace PetHome.Volunteers.Application.Database.Dto;
public class PetDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string AvatarUrl { get; set; }  = string.Empty;
    public MediaFile? Avatar {  get; init; }
    public IReadOnlyList<string> PhotosUrls { get; set; } = [];
    public IReadOnlyList<MediaFile> Photos { get; init; } = [];
    public Guid SpeciesId { get; init; }
    public string Description { get; init; }
    public Guid? BreedId { get; init; }
    public string Color { get; init; }
    public Guid ShelterId { get; init; }
    public double Weight { get; init; }
    public bool IsCastrated { get; init; }
    public DateTime? BirthDate { get; init; }
    public bool IsVaccinated { get; init; }
    public PetStatusEnum Status;
    public DateTime ProfileCreateDate { get; init; }
    public Guid VolunteerId { get; init; }
    public int SerialNumber { get; init; }
}
