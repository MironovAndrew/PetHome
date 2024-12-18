﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHome.Application.Database;
using PetHome.Application.Interfaces.RepositoryInterfaces;
using PetHome.Application.Validator;
using PetHome.Domain.PetManagment.VolunteerEntity;
using PetHome.Domain.Shared.Error;

namespace PetHome.Application.Features.Write.VolunteerManegment.SoftDeleteRestoreVolunteer;
public class SoftRestoreVolunteerUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<SoftRestoreVolunteerUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public SoftRestoreVolunteerUseCase(
        IVolunteerRepository volunteerRepository,
        ILogger<SoftRestoreVolunteerUseCase> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Execute(
        Guid id, CancellationToken ct)
    {
        var transaction = await _unitOfWork.BeginTransaction(ct);
        try
        {
            Volunteer volunteer = _volunteerRepository.GetById(id, ct).Result.Value;
            volunteer.SoftRestore();
            await _volunteerRepository.Update(volunteer, ct);

            await _unitOfWork.SaveChages(ct);
            transaction.Commit();

            _logger.LogInformation("Волонтёр с id = {0} и его сущности soft restored", id);
            return id;
        }
        catch (Exception)
        {
            transaction.Rollback();
            _logger.LogInformation("Не удалось восстановить волонтёра с id = {0}", id);
            return (ErrorList)Errors.Failure("Database.is.failed");
        }
    }
}