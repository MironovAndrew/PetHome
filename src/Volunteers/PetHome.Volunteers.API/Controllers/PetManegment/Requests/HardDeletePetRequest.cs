﻿using PetHome.Volunteers.Application.Features.Write.PetManegment.HardDelete;

namespace PetHome.Volunteers.API.Controllers.PetManegment.Requests;
public record HardDeletePetRequest(
    Guid VolunteerId,
    Guid PetId)
{
    public static implicit operator HardDeletePetCommand(HardDeletePetRequest request)
    {
        return new HardDeletePetCommand(request.VolunteerId, request.PetId);
    }
}
