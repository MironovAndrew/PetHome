﻿using CSharpFunctionalExtensions;
using NotificationService.Infrastructure.Database;
using NotificationService.Infrastructure.EmailNotification;
using NotificationService.Infrastructure.EmailNotification.EmailManagerImplementations;
using NotificationService.Infrastructure.TelegramNotification;
using PetHome.Core.Interfaces.FeatureManagment;
using PetHome.Core.Response.Validation.Validator;

namespace NotificationService.Application.Features.GeneralNotification.SendMessageEverywhere;

public class SendMessageEverywhereUseCase
    :ICommandHandler<SendMessageEverywhereCommand>
{
    private readonly NotificationRepository _repository;
    private readonly TelegramManager _telegramManager;
    private readonly UnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    //private readonly IGetVolunteerInformationContract _getVolunteerInformationContract;

    public SendMessageEverywhereUseCase(
        NotificationRepository repository,
        TelegramManager telegramManager,
        IConfiguration configuration,
        //IGetVolunteerInformationContract getVolunteerInformationContract,
        UnitOfWork unitOfWork)
    {
        _repository = repository;
        _telegramManager = telegramManager;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        //_getVolunteerInformationContract = getVolunteerInformationContract;
    }

    public async Task<UnitResult<ErrorList>> Execute(SendMessageEverywhereCommand command, CancellationToken ct)
    {
        var userNotificationSettings = await _repository.Get(command.UserId, ct);
        if (userNotificationSettings is null)
        {
            await _repository.Reset(command.UserId, ct);
            var transaction = await _unitOfWork.BeginTransaction(ct);
            await _unitOfWork.SaveChanges(ct);
            transaction.Commit();

            userNotificationSettings = await _repository.Get(command.UserId, ct);
        }

        if (userNotificationSettings?.IsWebSend == true)
        {
            //
        }

        if (userNotificationSettings?.IsEmailSend == true)
        {
            //TODO
            string email = "smirnay2001@mail.ru";
            //var userDto = await _getVolunteerInformationContract.Execute(command.UserId, ct);
            //if (userDto is null) 
            //    return;

            EmailManager emailManager = YandexEmailManager.Build(_configuration);
            emailManager.SendMessage(email, command.Subject, command.Body);
        }

        if (userNotificationSettings?.TelegramSettings != null)
        {
            await _telegramManager.SendMessage(command.UserId, command.Body);
        }
        else if (command.TelegramUserId != null)
        {
            await _telegramManager.StartRegisterChatId(command.UserId, command.TelegramUserId);
            await _telegramManager.SendMessage(command.UserId, command.Body);
        }

        return Result.Success<ErrorList>();
    } 
}
