﻿using CSharpFunctionalExtensions;
using FilesService.Core.Dto.File;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Core.Application.Interfaces.FeatureManagement;
using PetHome.Core.Infrastructure.Database;
using PetHome.Core.Web.Extentions.ErrorExtentions;
using PetHome.SharedKernel.Responses.ErrorManagement;
using PetManagementService.Application.Database;
using PetManagementService.Application.Database.Dto;
using PetManagementService.Domain.PetManagment.PetEntity;
using PetManagementService.Domain.PetManagment.VolunteerEntity;

namespace PetManagementService.Application.Features.Write.PetManegment.SetMainPhoto;
public class SetPetMainPhotoUseCase
    : ICommandHandler<SetPetMainPhotoCommand>
{
    private readonly IPetManagementReadDbContext _readDBContext;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<SetPetMainPhotoUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SetPetMainPhotoCommand> _validator;

    public SetPetMainPhotoUseCase(
        IPetManagementReadDbContext readDBContext,
        IVolunteerRepository volunteerRepository,
        ILogger<SetPetMainPhotoUseCase> logger,
         IUnitOfWork unitOfWork,
        IValidator<SetPetMainPhotoCommand> validator)
    {
        _readDBContext = readDBContext;
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Execute(
        SetPetMainPhotoCommand command,
        CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (validationResult.IsValid is false)
        {
            return validationResult.Errors.ToErrorList();
        }


        VolunteerDto? volunteerDto = _readDBContext.Volunteers
                    .FirstOrDefault(v => v.Id == command.VolunteerId);
        if (volunteerDto == null)
        {
            _logger.LogError("Волонтёр с id = {0} не найден", command.VolunteerId);
            return Errors.NotFound($"Волонтёр с id = {command.VolunteerId}").ToErrorList();
        }
         
        var getVolunteerResult = await _volunteerRepository
            .GetById(command.VolunteerId, ct);
        if (getVolunteerResult.IsFailure)
            return getVolunteerResult.Error.ToErrorList();

        Volunteer volunteer = getVolunteerResult.Value;
        Pet? pet = volunteer.Pets
            .FirstOrDefault(p => p.Id == command.PetId);
        if (pet == null)
        {
            _logger.LogError("Питомец с id = {0} не найдена", command.PetId);
            return Errors.NotFound($"Питомец с id = {command.PetId}").ToErrorList();
        }

        MediaFile media = MediaFile.Create(command.BucketName, command.FileName).Value;
        pet.SetMainPhoto(media);
        var transaction = await _unitOfWork.BeginTransaction(ct);

        await _volunteerRepository.Update(volunteer, ct);
        await _unitOfWork.SaveChanges(ct);
        transaction.Commit();

        string message = $"Главная фотография питомца = {command.PetId} успешно изменена";
        _logger.LogInformation(message);
        return Result.Success<ErrorList>();
    }
}
