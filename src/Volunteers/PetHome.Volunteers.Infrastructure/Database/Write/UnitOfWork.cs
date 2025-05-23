﻿using Microsoft.EntityFrameworkCore.Storage;
using PetHome.Framework.Database;
using PetHome.Volunteers.Infrastructure.Database.Write.DBContext;
using System.Data;

namespace PetHome.Volunteers.Infrastructure.Database.Write;
public class UnitOfWork : IUnitOfWork
{
    private readonly VolunteerWriteDbContext _dbContex;
    public UnitOfWork(VolunteerWriteDbContext dBContext)
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
