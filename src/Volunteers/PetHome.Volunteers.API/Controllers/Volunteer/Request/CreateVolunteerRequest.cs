﻿using PetHome.Volunteers.Application.Features.Write.VolunteerManegment.CreateVolunteer;

namespace PetHome.Volunteers.API.Controllers.Volunteer.Request;

public record CreateVolunteerRequest(
        FullNameDto FullNameDto,
        string Email,
        string Description,
        DateTime StartVolunteeringDate,
        IEnumerable<string> PhoneNumbers,
        IEnumerable<string> SocialNetworks,
        IEnumerable<RequisitesesDto> RequisitesesDto)
{
    public static implicit operator CreateVolunteerCommand(CreateVolunteerRequest request)
    {
        return new CreateVolunteerCommand(
            request.FullNameDto,
            request.Email,
            request.Description,
            request.StartVolunteeringDate,
            request.PhoneNumbers,
            request.SocialNetworks,
            request.RequisitesesDto);
    }
}

