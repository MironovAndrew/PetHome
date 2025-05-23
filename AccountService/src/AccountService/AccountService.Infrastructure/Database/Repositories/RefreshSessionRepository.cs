﻿using AccountService.Application.Database.Repositories;
using AccountService.Domain.Aggregates;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetHome.SharedKernel.Responses.ErrorManagement;
using PetHome.SharedKernel.Responses.RefreshToken;

namespace AccountService.Infrastructure.Database.Repositories;
public class RefreshSessionRepository
    : IRefreshSessionRepository
{
    private readonly AuthorizationDbContext _dbContext;
    public RefreshSessionRepository(AuthorizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(RefreshSession refreshSession, CancellationToken ct)
    {
        await _dbContext.RefreshSessions.AddAsync(refreshSession, ct);
    }

    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(
        Guid refreshToken, CancellationToken ct)
    {
        var result = await _dbContext.RefreshSessions
             .FirstOrDefaultAsync(r => r.RefreshToken == refreshToken, ct);

        if (result is null)
            return Errors.NotFound($"Refresh session с refresh token = {refreshToken}");

        return result;
    }

    public async Task<UnitResult<Error>> Remove(
        Guid id, CancellationToken ct)
    {
        var result = await _dbContext.RefreshSessions
             .FirstOrDefaultAsync(r => r.Id == id, ct);

        if (result is null)
            return Errors.NotFound($"Refresh session с id = {id}");

        _dbContext.RefreshSessions.Remove(result);
        return UnitResult.Success<Error>();
    }

    public async Task Remove(
        RefreshSession refreshSession, CancellationToken ct)
    {
        _dbContext.RefreshSessions.Remove(refreshSession);
    }

    public async Task RemoveOldWithSavingChanges(User user, CancellationToken ct)
    {
        var refreshSessionsToRemove = await _dbContext.RefreshSessions.Where(r => r.UserId == user.Id).ToListAsync(ct);
        _dbContext.RefreshSessions.RemoveRange(refreshSessionsToRemove);
    }

}
