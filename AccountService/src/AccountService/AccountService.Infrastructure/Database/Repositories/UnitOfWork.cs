﻿using Microsoft.EntityFrameworkCore.Storage;
using PetHome.Core.Infrastructure.Database;
using System.Data;

namespace AccountService.Infrastructure.Database.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AuthorizationDbContext _dbContex;
    public UnitOfWork(AuthorizationDbContext dBContext)
    {
        _dbContex = dBContext;
    }
    public async Task<IDbTransaction> BeginTransaction(CancellationToken ct = default)
    {
        var transaction = await _dbContex.Database.BeginTransactionAsync(ct);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken ct = default)
    {
        await _dbContex.SaveChangesAsync(ct);
    }
}
