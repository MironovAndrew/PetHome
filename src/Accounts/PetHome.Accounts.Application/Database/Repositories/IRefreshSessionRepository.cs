﻿using CSharpFunctionalExtensions;
using PetHome.Accounts.Domain.Aggregates;
using PetHome.Accounts.Domain.Tokens.RefreshToken;
using PetHome.Core.Response.ErrorManagment;

namespace PetHome.Accounts.Application.Database.Repositories;
public interface IRefreshSessionRepository
{
    public Task Add(RefreshSession refreshSession, CancellationToken ct);

    public Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid id, CancellationToken ct);

    public Task<UnitResult<Error>> Remove(Guid id, CancellationToken ct);

    public Task Remove(RefreshSession refreshSession, CancellationToken ct);

    public Task RemoveOldWithSavingChanges(User user, CancellationToken ct);
}
