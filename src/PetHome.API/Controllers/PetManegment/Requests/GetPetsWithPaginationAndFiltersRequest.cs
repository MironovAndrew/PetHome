﻿using PetHome.Application.Database.Dtos;
using PetHome.Application.Features.Read.PetManegment.Pet.GetPetsWithPaginationAndFilters;
using PetHome.Domain.PetManagment.PetEntity;

namespace PetHome.API.Controllers.PetManegment.Requests;

public record GetPetsWithPaginationAndFiltersRequest(Guid? VolunteerId,
    string? Name,
    int? Age,
    Guid? SpeciesId,
    Guid? BreedId,
    string? Color,
    Guid? ShelterId,
    double? Weight,
    bool? IsVaccinated,
    bool? IsCastrated,
    PetStatusEnum? Status,
    PagedListDto PagedListDto)
{
    public static implicit operator GetPetsWithPaginationAndFiltersQuery(GetPetsWithPaginationAndFiltersRequest request)
    {
        return new GetPetsWithPaginationAndFiltersQuery(
            request.VolunteerId,
            request.Name,
            request.Age,
            request.SpeciesId,
            request.BreedId,
            request.Color,
            request.ShelterId,
            request.Weight,
            request.IsVaccinated,
            request.IsCastrated,
            request.Status,
            request.PagedListDto);
    }
}
