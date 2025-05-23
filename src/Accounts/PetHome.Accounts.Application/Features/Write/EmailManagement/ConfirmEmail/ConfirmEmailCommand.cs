﻿using PetHome.Core.Interfaces.FeatureManagment;

namespace PetHome.Accounts.Application.Features.Write.EmailManagement.ConfirmEmail;
public record ConfirmEmailCommand(Guid UserId, string Token) : ICommand;