﻿using CSharpFunctionalExtensions;
using PetHome.Domain.GeneralValueObjects;
using PetHome.Domain.Shared.Error;
using PetHome.Domain.VolunteerEntity;

namespace PetHome.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerUseCase
{
    private readonly IVolunteerRepository VolunteerRepository;

    public CreateVolunteerUseCase(IVolunteerRepository volunteerRepository)
    {
        VolunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Execute(CreateVolunteerRequest request, CancellationToken ct)
    {  
        VolunteerId id = VolunteerId.Create();

        FullName fullName = FullName.Create(request.FirstName, request.LastName).Value;

        Email email = Email.Create(request.Email).Value;

        Date startVolunteeringDate = Date.Create(request.StartVolunteeringDate).Value;


        List<PhoneNumber> phoneNumberList = request.PhoneNumbers
                .Select(x => PhoneNumber.Create(x).Value)
                .ToList();
        PhoneNumbersDetails phoneNumberDetails = PhoneNumbersDetails.Create(phoneNumberList);


        List<SocialNetwork> socialNetworkList = request.SocialNetworks
                .Select(x => SocialNetwork.Create(x).Value)
                .ToList();
        SocialNetworkDetails socialNetworkDetails = SocialNetworkDetails.Create(socialNetworkList);


        List<Requisites> requisitesList = request.RequisitesesDto
                 .Select(x => Requisites.Create(x.Name, x.Desc, x.PaymentMethod).Value)
                 .ToList();
        RequisitesDetails requisitesDetails = RequisitesDetails.Create(requisitesList).Value;
          

        Volunteer volunteer = Volunteer.Create(
            id,
            fullName,
            email,
            request.Description,
            startVolunteeringDate,
            phoneNumberDetails,
            socialNetworkDetails,
            requisitesDetails)
            .Value;

        var result = await VolunteerRepository.Add(volunteer);

        return volunteer.Id.Value;
    }
}
