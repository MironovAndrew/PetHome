﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHome.Core.Constants;
using PetHome.Core.Extentions.ErrorExtentions;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Core.Response.ErrorManagment;
using PetHome.Core.Response.Validation.Validator;
using PetHome.Core.ValueObjects;
using PetHome.Framework.Database;
using PetHome.Volunteers.Application.Database;
using PetHome.Volunteers.Domain.PetManagment.VolunteerEntity;

namespace PetHome.Volunteers.Application.Features.Write.VolunteerManegment.CreateVolunteer;

public class CreateVolunteerUseCase
    : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;

    public CreateVolunteerUseCase(
        IVolunteerRepository volunteerRepository,
        ILogger<CreateVolunteerUseCase> logger,
       [FromKeyedServices(Constants.VOLUNTEER_UNIT_OF_WORK_KEY)] IUnitOfWork unitOfWork,
        IValidator<CreateVolunteerCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Execute(
        CreateVolunteerCommand createVolunteerCommand,
        CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(createVolunteerCommand, ct);
        if (validationResult.IsValid is false)
            return validationResult.Errors.ToErrorList();

        VolunteerId id = VolunteerId.Create().Value;

        FullName fullName = FullName.Create(
            createVolunteerCommand.FullNameDto.FirstName,
            createVolunteerCommand.FullNameDto.LastName).Value;

        Email email = Email.Create(createVolunteerCommand.Email).Value;

        Description description = Description.Create(createVolunteerCommand.Description).Value;

        Date startVolunteeringDate = Date.Create(createVolunteerCommand.StartVolunteeringDate).Value;

        List<PhoneNumber> phoneNumberList = createVolunteerCommand.PhoneNumbers
                .Select(x => PhoneNumber.Create(x).Value)
                .ToList();

        List<SocialNetwork> socialNetworkList = createVolunteerCommand.SocialNetworks
                .Select(x => SocialNetwork.Create(x).Value)
                .ToList();

        List<Requisites> requisitesList = createVolunteerCommand.RequisitesesDto
                 .Select(x => Requisites.Create(x.Name, x.Desc, x.PaymentMethod).Value)
                 .ToList();

        Volunteer volunteer = Volunteer.Create(
            id,
            fullName,
            email,
            description,
            startVolunteeringDate,
            phoneNumberList,
            requisitesList,
            socialNetworkList)
            .Value;

        var transaction = await _unitOfWork.BeginTransaction(ct);

        var result = await _volunteerRepository.Add(volunteer, ct);

        await _unitOfWork.SaveChanges(ct);
        transaction.Commit();

        _logger.LogInformation("Волонетёр с id = {0} был создан", volunteer.Id.Value);
        return volunteer.Id.Value;
    }
}
