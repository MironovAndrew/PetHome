﻿using PetHome.Core.Interfaces.FeatureManagment;

namespace PetHome.Accounts.Application.Features.UpdateAccessTokenUsingRefreshToken;
public record UpdateAccessTokenUsingRefreshTokenCommand(
    Guid RefreshToken,
    string AccessToken) : ICommand;
