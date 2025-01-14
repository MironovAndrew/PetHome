﻿using PetHome.Core.Interfaces.FeatureManagment;

namespace PetHome.Accounts.Application.Features.RegisterAccount;
public record RegisterParticipantUserCommand(string Email, string Name, string Password) : ICommand;
