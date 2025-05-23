﻿using PetManagementService.Application.Features.Read.Species.GetAllSpecies;

namespace PetManagementService.API.Controllers.Species.Requests;
public record GetAllSpeciesRequest(int PageNum, int PageSize)
{
    public static implicit operator GetAllSpeciesQuery(GetAllSpeciesRequest request)
        => new GetAllSpeciesQuery(request.PageNum, request.PageSize);
}
