﻿using PetHome.Volunteers.Application.Features.Write.PetManegment.DeleteSpeciesById;

namespace PetHome.Volunteers.API.Controllers.PetManagment.Requests;

public record DeleteSpeciesByIdRequest(Guid SpeciesId)
{
    public static implicit operator DeleteSpeciesByIdCommand(DeleteSpeciesByIdRequest request)
    {
        return new DeleteSpeciesByIdCommand(request.SpeciesId);
    }
}
