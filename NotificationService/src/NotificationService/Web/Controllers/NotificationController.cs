﻿using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Dto;
using NotificationService.Application.Features.GeneralNotification.SendMessageEverywhere;
using NotificationService.Application.Features.UsersNotificationSettings.GetAnyUsersNotificationSettings;
using NotificationService.Application.Features.UsersNotificationSettings.GetUserNotificationSettings;
using NotificationService.Application.Features.UsersNotificationSettings.GetUsersEmailSendings;
using NotificationService.Application.Features.UsersNotificationSettings.GetUsersTelegramSendings;
using NotificationService.Application.Features.UsersNotificationSettings.GetUsersWebSendings;
using NotificationService.Application.Features.UsersNotificationSettings.ResetUserNotificationSettings;
using NotificationService.Application.Features.UsersNotificationSettings.UpdateUserNotificationSettings;

namespace NotificationService.Web.Controllers;

public class NotificationController : ParentController
{

    [HttpGet("notification-settings")]
    public async Task<IActionResult> GetAnyUsersNotificationSettings(
        [FromServices] GetAnyUsersNotificationSettingsUseCase useCase,
        CancellationToken ct = default)
    {
        var result = await useCase.Execute(ct);

        return Ok(result);
    }


    [HttpGet("notification-settings/{userId:guid}")]
    public async Task<IActionResult> GetUserNotificationSettings(
        [FromRoute] Guid userId,
        [FromServices] GetUserNotificationSettingsUseCase useCase,
        CancellationToken ct = default)
    {
        GetUserNotificationSettingsQuery query = new(userId);
        var result = await useCase.Execute(query, ct);
        return Ok(result);
    }


    [HttpGet("notification-settings/all-email-sendings")]
    public async Task<IActionResult> GetUsersEmailSendings(
        [FromServices] GetUsersEmailSendingsUseCase useCase,
        CancellationToken ct = default)
    {
        var result = await useCase.Execute(ct);
        return Ok(result);
    }


    [HttpGet("notification-settings/all-telegram-sendings")]
    public async Task<IActionResult> GetUsersTelegramSendings(
        [FromServices] GetUsersTelegramSendingsUseCase useCase,
        CancellationToken ct = default)
    {
        var result = await useCase.Execute(ct);
        return Ok(result);
    }


    [HttpGet("notification-settings/all-web-sendings")]
    public async Task<IActionResult> GetUsersWebSendings(
        [FromServices] GetUsersWebSendingsUseCase useCase,
        CancellationToken ct = default)
    {
        var result = await useCase.Execute(ct);
        return Ok(result);
    }


    [HttpPost("notification-settings/{userId:guid}")]
    public async Task<IActionResult> ResetUserNotificationSettings(
        [FromRoute] Guid userId,
        [FromServices] ResetUserNotificationSettingsUseCase useCase,
        CancellationToken ct = default)
    {
        ResetUserNotificationSettingsCommand command = new(userId);
        await useCase.Execute(command, ct);
        return Ok();
    }


    [HttpPost("notification-settings/new/{userId:guid}")]
    public async Task<IActionResult> UpdateUserNotificationSettings(
        [FromRoute] Guid userId,
        [FromBody] SendingNotificationSettingsDto newNotificationSettings,
        [FromServices] UpdateUserNotificationSettingsUseCase useCase,
        CancellationToken ct = default)
    {
        UpdateUserNotificationSettingsCommand command = new(userId, newNotificationSettings);
        await useCase.Execute(command, ct);
        return Ok();
    }


    [HttpPost("notification-everywhere")]
    public async Task<IActionResult> SendMessageEverywhere( 
        [FromServices] SendMessageEverywhereUseCase useCase,
        [FromBody] SendMessageEverywhereCommand command,
        CancellationToken ct = default)
    {
        await useCase.Execute(command, ct);
        return Ok();
    }
}
