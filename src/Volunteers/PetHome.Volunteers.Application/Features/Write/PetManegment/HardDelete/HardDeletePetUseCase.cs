﻿using CSharpFunctionalExtensions;
using FilesService.Core.Dto.File;
using FilesService.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHome.Core.Constants;
using PetHome.Core.Extentions.ErrorExtentions;
using PetHome.Core.Interfaces;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Core.Response.ErrorManagment;
using PetHome.Core.Response.Validation.Validator;
using PetHome.Framework.Database;
using PetHome.Volunteers.Application.Database;
using PetHome.Volunteers.Application.Database.Dto;
using PetHome.Volunteers.Application.Features.Write.PetManegment.ChangePetInfo;
using PetHome.Volunteers.Domain.PetManagment.PetEntity;
using PetHome.Volunteers.Domain.PetManagment.VolunteerEntity;

namespace PetHome.Volunteers.Application.Features.Write.PetManegment.HardDelete;
public class HardDeletePetUseCase
    : ICommandHandler<HardDeletePetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IVolunteerReadDbContext _readDBContext;
    private readonly IMinioFilesHttpClient _filesProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ChangePetInfoUseCase> _logger;

    public HardDeletePetUseCase(
         IVolunteerRepository volunteerRepository,
         IVolunteerReadDbContext readDBContext,
         IMinioFilesHttpClient filesProvider,
        [FromKeyedServices(Constants.VOLUNTEER_UNIT_OF_WORK_KEY)] IUnitOfWork unitOfWork,
         ILogger<ChangePetInfoUseCase> logger)
    {
        _volunteerRepository = volunteerRepository;
        _readDBContext = readDBContext;
        _filesProvider = filesProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<UnitResult<ErrorList>> Execute(
        HardDeletePetCommand command,
        CancellationToken ct)
    {
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

        var transaction = await _unitOfWork.BeginTransaction(ct);

        volunteer.Pets.Remove(pet);
        await _volunteerRepository.Update(volunteer, ct);
        await _unitOfWork.SaveChanges(ct);
        transaction.Commit();

        if (pet.Photos.Count > 0)
        {
            List<MinioFileName> minioFileNames = pet.Photos
                .Select(f => MinioFileName.Create(f.FileName).Value)
                .ToList();
            string bucketName = pet.Photos.Select(f => f.BucketName).First();
            MinioFilesInfoDto minioFileInfoDto = new MinioFilesInfoDto(bucketName, minioFileNames);
            await _filesProvider.DeleteFile(minioFileInfoDto, ct);
        }
        string message = $"Питомец = {command.PetId} успешно hard deleted!";
        _logger.LogInformation(message);
        return Result.Success<ErrorList>();
    }
}
