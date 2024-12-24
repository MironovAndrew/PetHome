﻿using PetHome.Application.Features.Read.PetManegment.Breeds.GetAllBreedDtoBySpeciesId;

namespace PetHome.API.Controllers.PetManegment.Requests;

public record GetAllBreedDtoBySpeciesIdRequest(Guid SpeciesId)
{
    public static implicit operator GetAllBreedDtoBySpeciesIdQuery(GetAllBreedDtoBySpeciesIdRequest request)
    {
        return new GetAllBreedDtoBySpeciesIdQuery(request.SpeciesId);
    }
}