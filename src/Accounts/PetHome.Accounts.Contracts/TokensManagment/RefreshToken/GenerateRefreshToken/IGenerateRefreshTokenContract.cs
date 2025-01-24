﻿using CSharpFunctionalExtensions;
using PetHome.Core.Response.ErrorManagment;
using PetHome.Core.Response.RefreshToken;
using PetHome.Core.ValueObjects.User;

namespace PetHome.Accounts.Contracts.TokensManagment.AccessToken.GenerateAccessToken;
public interface IGenerateRefreshTokenContract
{
    public Task<Result<RefreshSession, Error>> Execute(UserId userId, CancellationToken ct);
}
