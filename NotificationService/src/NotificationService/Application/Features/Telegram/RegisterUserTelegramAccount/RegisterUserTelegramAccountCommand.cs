﻿using PetHome.Core.Application.Interfaces.FeatureManagement;

namespace NotificationService.Application.Features.Telegram.RegisterUserTelegramAccount;

public record RegisterUserTelegramAccountCommand(
    Guid UserId, string UserTelegramId) : ICommand;
