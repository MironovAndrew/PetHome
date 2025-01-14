﻿using PetHome.Accounts.Application.Features.UpdateAccessTokenUsingRefreshToken;

namespace PetHome.Accounts.API.Controllers.Requests;
public record UpdateAccessTokenUsingRefreshTokenRequest(Guid RefreshToken, string AccessToken)
{
    public static implicit operator UpdateAccessTokenUsingRefreshTokenCommand(
        UpdateAccessTokenUsingRefreshTokenRequest request)
    {
        return new UpdateAccessTokenUsingRefreshTokenCommand(request.RefreshToken, request.AccessToken);
    }
}
